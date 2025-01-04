using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] float speed = 3.0f; // �ړ����x
    [SerializeField] float stopDistance = 2.0f; // ��~����
    [SerializeField] float rotationSpeed = 5.0f; // ��]�̊��炩��
    [SerializeField] Transform player; // �v���C���[��Transform
    [SerializeField] GameObject Mannequin;//�}�l�L��
    bool chase = false;


    void Update()
    {
        if (player == null) return;

        // �v���C���[�Ƃ̋������v�Z
        float distanceToPlayer = Vector3.Distance(Mannequin.transform.position, player.position);

        // ��~������艓���ꍇ�ɒǏ]
        if (distanceToPlayer > stopDistance)
        {
            // �v���C���[�ւ̕������v�Z
            Vector3 direction = (player.position - Mannequin.transform.position).normalized;

            // �V�����ʒu���v�Z
            Vector3 targetPosition = Mannequin.transform.position + direction * speed * Time.deltaTime;

            // �V�����ʒu���X���[�Y�ɕ��
            Mannequin.transform.position = Vector3.Lerp(Mannequin.transform.position, targetPosition, 0.5f);

            // �v���C���[�����������i��]���X���[�Y�Ɂj
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer < stopDistance)
        {
            chase = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            chase = true;
        }
    }
}
