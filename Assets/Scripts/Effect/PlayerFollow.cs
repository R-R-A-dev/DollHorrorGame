using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] float speed = 3.0f; // 移動速度
    [SerializeField] float stopDistance = 2.0f; // 停止距離
    [SerializeField] float rotationSpeed = 5.0f; // 回転の滑らかさ
    [SerializeField] Transform player; // プレイヤーのTransform
    [SerializeField] GameObject Mannequin;//マネキン
    bool chase = false;


    void Update()
    {
        if (player == null) return;

        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(Mannequin.transform.position, player.position);

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
