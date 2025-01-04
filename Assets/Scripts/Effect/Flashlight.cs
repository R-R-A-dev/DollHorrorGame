using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light light;
    public Transform target; // 追従するターゲット（親オブジェクトのカメラ）
    public float followSpeed = 5.0f; // 追従速度


    private Quaternion targetRotation; // 目標の回転
    private Vector3 targetPosition;    // 目標の位置

    void Update()
    {

        // ターゲット（カメラ）の現在位置と回転を取得
        targetPosition = target.position ;
        targetRotation = target.rotation;

        // 現在のライトの位置と回転を目標にスムーズに補間
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
    }
}
