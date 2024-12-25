using System.Collections;
using System.Collections.Generic;
using TriangleNet.Topology.DCEL;
using UnityEngine;

namespace effects
{
    public class CockroachSpawner : MonoBehaviour
    {
        [SerializeField] GameObject cockroachPrefab; // 虫のプレハブ
        [SerializeField] Transform[] dollsTransform; // 人形のTransform
        [SerializeField] Transform parent;
        void Start()
        {
            for (int i = 0; i < dollsTransform.Length; i++)
            {
                GameObject cockroach = Instantiate(cockroachPrefab, dollsTransform[i].position, Quaternion.identity, parent);
            }
        }

    }
}