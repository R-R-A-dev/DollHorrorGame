using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerFollow : MonoBehaviour
{
    #region
    [SerializeField] float speed = 3.0f; // 移動速度
    [SerializeField] float stopDistance = 1.4f; // 停止距離
    [SerializeField] float rotationSpeed = 5.0f; // 回転の滑らかさ
    [SerializeField] GameObject fpsController; // プレイヤーオブジェクト取得
    [SerializeField] GameObject Mannequin;//マネキン
    [SerializeField] GameObject MannequinChild;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    private Animator MannequinAnim;
    private Transform player; // プレイヤーのTransform
    private bool chase = false;
    private bool isPlayingBiteAnimation = false;
    public float distanceToPlayer;
    public float distanceFromSurface = 0.1f;
    

    public Transform target; // 向くべきターゲットオブジェクト
    public float duration = 0.15f; // 回転にかける時間
    private Quaternion initialRotation; // 初期の回転
    private Quaternion targetRotation; // 目標の回転
    private float elapsedTime = 0f; // 経過時間
    private bool isRotating = false; // 回転中かどうか
    private bool isStartLookAt = false; // プレイヤーを見るかどうか

    private float animFrame = 0.224f;

    #endregion

    private void Start()
    {
        // プレイヤーのTransformを取得
        player = fpsController.transform;
        MannequinAnim = MannequinChild.GetComponent<Animator>();
        CinemachineImpulseSource cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {


        if (player == null) return;

        // プレイヤーとの距離を計算
        distanceToPlayer = Vector3.Distance(Mannequin.transform.position, player.position);

        AlignToGround();

        // 停止距離より遠い場合に追従
        if (distanceToPlayer > stopDistance)
        {
            // プレイヤーへの方向を計算
            Vector3 direction = (player.position - Mannequin.transform.position).normalized;

            // 新しい位置を計算
            Vector3 targetPosition = Mannequin.transform.position + direction * speed * Time.deltaTime;

            // 新しい位置をスムーズに補間
            Mannequin.transform.position = Vector3.Lerp(Mannequin.transform.position, targetPosition, 0.5f);

            // プレイヤー方向を向く（回転をスムーズに）
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Mannequin.transform.rotation = Quaternion.Lerp(Mannequin.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            isPlayingBiteAnimation = false;
        }
        else if (distanceToPlayer < stopDistance && !isPlayingBiteAnimation)
        {
            if (!isStartLookAt) StartLookAt();
            // プレイヤーの方に向く
            if (isRotating)
            {
                // 回転処理
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);

                // 初期回転から目標回転へ補間
                fpsController.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
                // 回転終了の判定
                if (t >= 1.0f)
                {
                    isRotating = false;
                }
            }
            else
            {// プレイヤーが近づいたら捕まえる
                isPlayingBiteAnimation = true;
                Grab();
                StartCoroutine(ShakeCamera());
            }
        }
    }

    private void AlignToGround()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Mannequin.transform.position, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            // 地形の法線を取得
            Vector3 terrainNormal = hitInfo.normal;

            // 地形の高さに合わせる
            Vector3 newPos = Mannequin.transform.position;
            newPos.y = hitInfo.point.y + distanceFromSurface;
            Mannequin.transform.position = newPos;

            // 地形の法線を使って回転を補正しつつ直立を維持
            Quaternion groundRotation = Quaternion.FromToRotation(Mannequin.transform.up, terrainNormal) * Mannequin.transform.rotation;
            Mannequin.transform.rotation = Quaternion.Lerp(Mannequin.transform.rotation, groundRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Grab()
    {
        // プレイヤーとの距離が一定以下の場合にプレイヤーを捕まえる
        if (distanceToPlayer < stopDistance)
        {
            // プレイヤーを捕まえる処理
            MannequinAnim.Play("Zombie Neck Bite", 0, animFrame);
        }
    }

    private void GameOver()
    {
        // ゲームオーバー処理
        Debug.Log("GameOver");
    }

    /// <summary>
    /// 捕まってプレイヤーの向きを変える
    /// </summary>
    void StartLookAt()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned.");
            return;
        }

        Vector3 lookPos = target.transform.position;
        lookPos.y += 0.7f;
        // 初期回転と目標回転を設定
        initialRotation = fpsController.transform.rotation;
        targetRotation = Quaternion.LookRotation(lookPos - fpsController.transform.position);

        // 回転処理を開始
        elapsedTime = 0f;
        isRotating = true;
        isStartLookAt = true;

        FirstPersonController.freezeTrg = true;
    }


    /// <summary>
    /// 捕まってカメラが揺れるメソッド
    /// </summary>
    IEnumerator ShakeCamera()
    {
        cinemachineVirtualCamera.enabled = true;
        yield return new WaitForSeconds(0.8f);
        cinemachineVirtualCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            chase = true;
        }
    }
}
