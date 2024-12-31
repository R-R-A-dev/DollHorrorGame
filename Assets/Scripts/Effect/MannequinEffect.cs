using effect.mannequin;
using effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void Falling()
        {

        }

        public void Grab()
        {

        }

        public void Appear(string trgName)
        {
            //分岐で演出を分けて出す
            MannequinMove.MoveEffectHandler(trgName);
        }


        private void OnTriggerEnter(Collider other)
        {
            //TODO: プレイヤーの角度を取得
            Appear(other.gameObject.name);
            other.gameObject.SetActive(false);
        }

    }
}

