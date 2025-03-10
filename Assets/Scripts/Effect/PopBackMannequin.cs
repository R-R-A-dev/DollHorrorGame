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
    [SerializeField] GameObject fpsController; // �v���C���[�I�u�W�F�N�g�擾
    [SerializeField] GameObject MannequinChild;
    [SerializeField] float rotationSpeed = 5.0f; // ��]�̊��炩��
    [SerializeField] GameObject fade;
    [SerializeField] GameObject chaseObj;
    [SerializeField] GameObject chaseTrg;
    [SerializeField] AudioSource impactSfx;
    [SerializeField] AudioSource environmentSfx;

    private float environmentVolume;

    public float distanceBehind = 2f; // �v���C���[����̋���
    public float heightOffset = 0.0f; // �����̃I�t�Z�b�g
    public float followSpeed = 20.0f; // �I�u�W�F�N�g���Ǐ]���鑬�x
    public bool chase = true;
    public float distanceFromSurface = 0.1f;

    private Vector3 targetPosition;
    private Animator MannequinAnim;


    public Transform target; // �����ׂ��^�[�Q�b�g�I�u�W�F�N�g
    private Quaternion initialRotation; // �����̉�]
    private Quaternion targetRotation; // �ڕW�̉�]
    private Transform player; // �v���C���[��Transform
    public bool isRotating = false; // ��]�����ǂ���
    private bool isStartLookAt = false; // �v���C���[�����邩�ǂ���
    private float elapsedTime = 0f; // �o�ߎ���
    private float animFrame = 0.224f;


    private bool isPlayingBiteAnimation = false;

    public float duration = 0.15f; // ��]�ɂ����鎞��
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

            // �v���C���[�̌��̃^�[�Q�b�g�ʒu���v�Z
            Vector3 behindPosition = player.position - player.forward * distanceBehind;


            // ���݂̈ʒu����^�[�Q�b�g�ʒu�ɃX���[�Y�Ɉړ�
            targetPosition = Vector3.Lerp(target.position, behindPosition, followSpeed * Time.deltaTime);
            targetPosition.y = player.position.y - tall;
            // �I�u�W�F�N�g���^�[�Q�b�g�ʒu�Ɉړ�
            target.position = targetPosition;

            // �v���C���[�̕����ɍ��킹�ĉ�]
            target.rotation = Quaternion.Lerp(target.rotation, player.rotation, followSpeed * Time.deltaTime);


        }


        // ��]����
        if (isPlayerLooking)
        {
            if (!isStartLookAt) StartLookAt();
            // �v���C���[�̕��Ɍ���
            if (isRotating)
            {
                // ��]����
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);

                // ������]����ڕW��]�֕��
                fpsController.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

                // ��]�I���̔���
                if (t >= 1.0f)
                {
                    isRotating = false;
                    environmentSoundOn();
                    impactSfx.Play();
                    fade.SetActive(true);
                }
            }
            else
            {// �v���C���[���߂Â�����߂܂���
                Grab();
                StartCoroutine(ShakeCamera());
            }
        }
    }

    IEnumerator DelayedGrab()
    {
        yield return new WaitForSeconds(2f); // 0.5�b��ɕ߂܂鉉�o���J�n
        Grab();
        StartCoroutine(ShakeCamera());
        //�Ó]�ȍ~�̉��o�L��
    }

    private void AlignToGround()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(target.transform.position, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            // �n�`�̖@�����擾
            Vector3 terrainNormal = hitInfo.normal;

            // �n�`�̍����ɍ��킹��
            Vector3 newPos = target.transform.position;
            newPos.y = hitInfo.point.y + distanceFromSurface;
            target.transform.position = newPos;

            // �n�`�̖@�����g���ĉ�]��␳���������ێ�
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
    /// �߂܂��ăv���C���[�̌�����ς���
    /// </summary>
    void StartLookAt()
    {
        if (target == null)
        {
            return;
        }

        Vector3 lookPos = target.transform.position;
        lookPos.y += 0.7f;
        // ������]�ƖڕW��]��ݒ�
        initialRotation = fpsController.transform.rotation;
        targetRotation = Quaternion.LookRotation(lookPos - fpsController.transform.position);

        // ��]�������J�n
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
        // �v���C���[��߂܂��鏈��
        MannequinAnim.Play("Zombie Neck Bite", 0, animFrame);

    }
}
