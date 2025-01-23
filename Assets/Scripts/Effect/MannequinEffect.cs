using effect.mannequin;
using effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace effect
{
    public class MannequinEffect : Effect
    {


        [SerializeField] MannequinMove MannequinMove;
        void Start()
        {

        }

        void Update()
        {

        }
        public override void PlayEffect()
        {
            throw new System.NotImplementedException();
        }
        //TODO: 演出メソッドの記載
        public void ChasePlayer()
        {

        }

        public void Falling(string trgName, Transform rotate)
        {

        }

        public void Grab()
        {

        }

        public void Appear(string trgName, Transform rotate)
        {

        }


        private void OnTriggerEnter(Collider other)
        {
            //TODO: トリガーの判定消す
            MannequinMove.MoveEffectHandler(other.gameObject.name, GetComponent<Transform>());
        }

    }

    [System.Serializable]
    public class ChildObject
    {
        public GameObject[] MannequinArray;
    }
}

