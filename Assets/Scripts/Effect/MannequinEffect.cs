using effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effect
{
    public class MannequinEffect : Effect
    {
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

        public void Appear()
        {
            //分岐で演出を分けて出す

            //演出1 マネキンの出現場所 飼い中電との明滅からの目前に出現
            if (true)
            {

            }
            else if (true)
            {

            }

            //演出2



        }


        private void OnTriggerEnter(Collider other)
        {
            
        }

    }
}

