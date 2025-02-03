using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerFollow : MonoBehaviour
{
    #region
    [SerializeField] float speed = 5.0f; // 移動速度
    [SerializeField] float stopDistance = 1.6f; // 停止距離
    [SerializeField] float rotationSpeed = 5.0f; // 回転の滑らかさ
    [SerializeField] GameObject fpsController; // プレイヤーオブジェクト取得
    [SerializeField] GameObject mannequin;//マネキン
    [SerializeField] GameObject mannequinChild;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] GameObject chaseObj;
    [SerializeField] PopBackMannequin PopBackMannequin;
    private Animator MannequinAnim;
    private Transform player; // プレイヤーのTransform
    private bool chase = false;
    private bool isPlayingBiteAnimation = false;
    public float distanceToPlayer;
    public float distanceFromSurface = 0.1f;

    public float briskWalkDistance = 10f;
    public Transform target; // 向くべきターゲットオブジェクト
    public float duration = 0.15f; // 回転にかける時間
    private Quaternion initialRotation; // 初期の回転
    private Quaternion targetRotation; // 目標の回転
    private float elapsedTime = 0f; // 経過時間
    private bool isRotating = false; // 回転中かどうか
    private bool isStartLookAt = false; // プレイヤーを見るかどうか

    private float animFrame = 0.224f;

    public float detectionAngle = 60f;// プレイヤーが振り向いていると判定する角度（度数）
    bool isPlayerLooking = false;
    public float l = 0;
    private bool isLookingAtEnemy = false;

    private bool isSoundPlaying = false;
    private bool isRunning = false;
    [SerializeField] private AudioSource[] mannequinFootStepSounds;
    [SerializeField] private AudioSource[] mannequinRunSounds;

    bool isAppear = false;
    private float appearElapseTime = 0;

    [SerializeField] GameObject backMannequin;
    #endregion

    private void Start()
    {
        // プレイヤーのTransformを取得
        player = fpsController.transform;
        MannequinAnim = mannequinChild.GetComponent<Animator>();
        CinemachineImpulseSource cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {

        if (!chase) return;
        if (isAppear)
        {
            appearElapseTime += Time.deltaTime;
            if (appearElapseTime > 30f)
            {
                isSoundPlaying = true;
                chase = true;
                chaseObj.SetActive(false);
                isAppear = false;
                isRunning = false;
                backMannequin.SetActive(false); 
            }
        }
        // プレイヤーが敵を見ているかを判定
        if (IsLookingAtEnemy())
        {
            //振り向いて音が止まる
            if (isSoundPlaying)
            {
                //音を止める
                isSoundPlaying = false;
            }
            //マネキンが走っている最中に振り向いたとき
            if (isRunning && isAppear)
            {
                //マネキンが消える
                chase = false;
                mannequin.SetActive(false);
                //背後に出てきて演出
                StartCoroutine(DelayedGrab());
            }
            return;
        }
        else
        {
            if (!isSoundPlaying)
            {
                //後ろから歩いてくる音を再生
                isSoundPlaying = true;
                StartCoroutine(MannequinFootSounds());
            }
        }

        if (player == null) return;

        // プレイヤーとの距離を計算
        distanceToPlayer = Vector3.Distance(mannequin.transform.position, player.position);

        AlignToGround();

        // 停止距離より遠い場合に追従
        if (distanceToPlayer > stopDistance)
        {
            // プレイヤーへの方向を計算
            Vector3 direction = (player.position - mannequin.transform.position).normalized;

            // 新しい位置を計算
            Vector3 targetPosition = mannequin.transform.position + direction * speed * Time.deltaTime;

            // 新しい位置をスムーズに補間
            mannequin.transform.position = Vector3.Lerp(mannequin.transform.position, targetPosition, 0.5f);

            // プレイヤー方向を向く（回転をスムーズに）
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            mannequin.transform.rotation = Quaternion.Lerp(mannequin.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            isPlayingBiteAnimation = false;
            if (distanceToPlayer < briskWalkDistance)
            {
                isRunning = true;
            }


        }
        else if (distanceToPlayer < stopDistance && !isPlayingBiteAnimation)
        {
            /*            if (!isStartLookAt) StartLookAt();
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
                        }*/
        }
    }
    IEnumerator DelayedGrab()
    {
        yield return new WaitForSeconds(1.8f); // 0.5秒後に捕まる演出を開始
        PopBackMannequin.chase = false;
        PopBackMannequin.isPlayerLooking = true;
    }

    private void AlignToGround()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(mannequin.transform.position, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            // 地形の法線を取得
            Vector3 terrainNormal = hitInfo.normal;

            // 地形の高さに合わせる
            Vector3 newPos = mannequin.transform.position;
            newPos.y = hitInfo.point.y + distanceFromSurface;
            mannequin.transform.position = newPos;

            // 地形の法線を使って回転を補正しつつ直立を維持
            Quaternion groundRotation = Quaternion.FromToRotation(mannequin.transform.up, terrainNormal) * mannequin.transform.rotation;
            mannequin.transform.rotation = Quaternion.Lerp(mannequin.transform.rotation, groundRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator MannequinFootSounds()
    {
        while (isSoundPlaying&& isAppear)
        {
            if (isRunning)
            {
                //走る足音を鳴らす
                mannequinFootStepSounds[0].Play();
                yield return new WaitForSeconds(0.3f);
                mannequinFootStepSounds[1].Play();
                yield return new WaitForSeconds(0.3f);
            }
            else
            {

                //歩く足音を鳴らす
                mannequinFootStepSounds[0].Play();
                yield return new WaitForSeconds(0.6f);
                mannequinFootStepSounds[1].Play();
                yield return new WaitForSeconds(0.6f);
            }
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
    /// 捕まえてプレイヤーの向きを変える
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

    public bool IsLookingAtEnemy()
    {
        // カメラの前方向
        Vector3 forward = fpsController.transform.forward;

        // プレイヤーから敵への方向
        Vector3 toEnemy = (target.position - fpsController.transform.position).normalized;

        // プレイヤーが敵の方を向いているかを判定
        float dot = Vector3.Dot(forward, toEnemy);

        // Dot値を角度に変換し、detectionAngle以内かを確認
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        l = angle;
        return angle <= detectionAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAppear = true;
            chase = true;
            chaseObj.SetActive(true);
            backMannequin.SetActive(true);
        }
    }
}

/*
 * 音声と演出全般の実装
 * 蚊柱
 * 
*/