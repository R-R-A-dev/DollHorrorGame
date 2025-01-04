using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light light;
    public Transform target; // �Ǐ]����^�[�Q�b�g�i�e�I�u�W�F�N�g�̃J�����j
    public float followSpeed = 5.0f; // �Ǐ]���x


    private Quaternion targetRotation; // �ڕW�̉�]
    private Vector3 targetPosition;    // �ڕW�̈ʒu

    void Update()
    {

        // �^�[�Q�b�g�i�J�����j�̌��݈ʒu�Ɖ�]���擾
        targetPosition = target.position ;
        targetRotation = target.rotation;

        // ���݂̃��C�g�̈ʒu�Ɖ�]��ڕW�ɃX���[�Y�ɕ��
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
    }
}
