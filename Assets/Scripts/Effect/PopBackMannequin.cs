using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PopBackMannequin : MonoBehaviour
{
    #region
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] GameObject fpsController; // プレイヤーオブジェクト取得
    [SerializeField] GameObject MannequinChild;
    [SerializeField] float rotationSpeed = 5.0f; // 回転の滑らかさ
    [SerializeField] GameObject fade;
    [SerializeField] GameObject chaseObj;
    [SerializeField] GameObject chaseTrg;
    [SerializeField] AudioSource impactSfx;
    [SerializeField] AudioSource environmentSfx;

    private float environmentVolume;

    public float distanceBehind = 2f; // プレイヤーからの距離
    public float heightOffset = 0.0f; // 高さのオフセット
    public float followSpeed = 20.0f; // オブジェクトが追従する速度
    public bool chase = true;
    public float distanceFromSurface = 0.1f;

    private Vector3 targetPosition;
    private Animator MannequinAnim;


    public Transform target; // 向くべきターゲットオブジェクト
    private Quaternion initialRotation; // 初期の回転
    private Quaternion targetRotation; // 目標の回転
    private Transform player; // プレイヤーのTransform
    public bool isRotating = false; // 回転中かどうか
    private bool isStartLookAt = false; // プレイヤーを見るかどうか
    private float elapsedTime = 0f; // 経過時間
    private float animFrame = 0.224f;


    private bool isPlayingBiteAnimation = false;

    public float duration = 0.15f; // 回転にかける時間
    public bool isPlayerLooking = false;
    float tall = 1.11f;
    #endregion

    private void Start()
    {
        player = fpsController.transform;
        MannequinAnim = MannequinChild.GetComponent<Animator>();
        CinemachineImpulseSource cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

    }

    void Update()
    {
        if (chase)
        {

            // プレイヤーの後ろのターゲット位置を計算
            Vector3 behindPosition = player.position - player.forward * distanceBehind;


            // 現在の位置からターゲット位置にスムーズに移動
            targetPosition = Vector3.Lerp(target.position, behindPosition, followSpeed * Time.deltaTime);
            targetPosition.y = player.position.y - tall;
            // オブジェクトをターゲット位置に移動
            target.position = targetPosition;

            // プレイヤーの方向に合わせて回転
            target.rotation = Quaternion.Lerp(target.rotation, player.rotation, followSpeed * Time.deltaTime);


        }


        // 回転処理
        if (isPlayerLooking)
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
                    environmentSoundOn();
                    impactSfx.Play();
                    fade.SetActive(true);
                }
            }
            else
            {// プレイヤーが近づいたら捕まえる
                Grab();
                StartCoroutine(ShakeCamera());
            }
        }
    }

    IEnumerator DelayedGrab()
    {
        yield return new WaitForSeconds(2f); // 0.5秒後に捕まる演出を開始
        Grab();
        StartCoroutine(ShakeCamera());
        //暗転以降の演出記載
    }

    private void AlignToGround()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(target.transform.position, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            // 地形の法線を取得
            Vector3 terrainNormal = hitInfo.normal;

            // 地形の高さに合わせる
            Vector3 newPos = target.transform.position;
            newPos.y = hitInfo.point.y + distanceFromSurface;
            target.transform.position = newPos;

            // 地形の法線を使って回転を補正しつつ直立を維持
            Quaternion groundRotation = Quaternion.FromToRotation(target.transform.up, terrainNormal) * target.transform.rotation;
            target.transform.rotation = Quaternion.Lerp(target.transform.rotation, groundRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void environmentSoundOff()
    {
        environmentSfx.volume = environmentVolume;
    }
    private void environmentSoundOn()
    {
        environmentVolume = environmentSfx.volume;
        environmentSfx.volume = 0f;
    }

    /// <summary>
    /// 捕まえてプレイヤーの向きを変える
    /// </summary>
    void StartLookAt()
    {
        if (target == null)
        {
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

    IEnumerator ShakeCamera()
    {
        cinemachineVirtualCamera.enabled = true;
        yield return new WaitForSeconds(1f);
        FirstPersonController.freezeTrg = false;
        cinemachineVirtualCamera.enabled = false;
        chaseObj.SetActive(false);
        chaseTrg.SetActive(false);
        this.enabled = false;
        environmentSoundOff();
    }

    void Grab()
    {
        // プレイヤーを捕まえる処理
        MannequinAnim.Play("Zombie Neck Bite", 0, animFrame);

    }
}
