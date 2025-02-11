using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoGroup : MonoBehaviour
{
    [Header("Swarm Settings")]
    [SerializeField] int swarmCount = 50;
    [SerializeField] float swarmRadius = 3f;
    [SerializeField] float baseSpeed = 0.5f;
    [SerializeField] float speedVariation = 0.3f;
    [SerializeField] float noiseScale = 0.5f;
    [SerializeField] float heightVariation = 2f;
    [SerializeField] float rotationSpeed = 2f;

    [Header("References")]
    [SerializeField] GameObject mosquitoPrefab;

    private Transform[] mosquitoes;
    private Vector3[] offsets;
    private float[] individualSpeeds;

    void Start()
    {
        InitializeSwarm();
    }

    void InitializeSwarm()
    {
        mosquitoes = new Transform[swarmCount];
        offsets = new Vector3[swarmCount];
        individualSpeeds = new float[swarmCount];

        for (int i = 0; i < swarmCount; i++)
        {
            // 蚊のインスタンス化
            GameObject mosquito = Instantiate(
                mosquitoPrefab,
                transform.position + Random.insideUnitSphere * swarmRadius,
                Quaternion.Euler(0, Random.Range(0, 360f), 0)
            );

            mosquito.transform.SetParent(transform);
            mosquitoes[i] = mosquito.transform;

            // 個体ごとのパラメータ設定
            offsets[i] = new Vector3(
                Random.Range(-100f, 100f),
                Random.Range(-100f, 100f),
                Random.Range(-100f, 100f)
            );

            individualSpeeds[i] = 1000 * baseSpeed + Random.Range(-speedVariation, speedVariation);
        }
    }

    void Update()
    {
        if (mosquitoes == null) return;

        for (int i = 0; i < swarmCount; i++)
        {
            if (mosquitoes[i] == null) continue;

            // ノイズベースの位置計算
            float noiseX = Mathf.PerlinNoise(Time.time * noiseScale, offsets[i].x);
            float noiseY = Mathf.PerlinNoise(Time.time * noiseScale, offsets[i].y);
            float noiseZ = Mathf.PerlinNoise(Time.time * noiseScale, offsets[i].z);

            Vector3 targetOffset = new Vector3(
                (noiseX - 0.5f) * 2f * swarmRadius,
                (noiseY - 0.5f) * heightVariation,
                (noiseZ - 0.5f) * 2f * swarmRadius
            );

            // スムーズな移動
            Vector3 targetPosition = transform.position + targetOffset;
            mosquitoes[i].position = Vector3.Lerp(
                mosquitoes[i].position,
                targetPosition,
                individualSpeeds[i] * Time.deltaTime
            );

            // 自然な回転処理
            Quaternion targetRotation = Quaternion.LookRotation(
                targetPosition - mosquitoes[i].position
            );

            mosquitoes[i].rotation = Quaternion.Slerp(
                mosquitoes[i].rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, swarmRadius);
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, swarmRadius);
    }
}
