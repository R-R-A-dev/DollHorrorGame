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
        //TODO: ���o���\�b�h�̋L��
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
            //����ŉ��o�𕪂��ďo��
            MannequinMove.MoveEffectHandler(trgName);
        }


        private void OnTriggerEnter(Collider other)
        {
            //TODO: �v���C���[�̊p�x���擾
            Appear(other.gameObject.name);
            other.gameObject.SetActive(false);
        }

    }
}

