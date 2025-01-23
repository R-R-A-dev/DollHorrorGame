using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

namespace effects
{
    public class OwnDollEffect : Effect
    {
        #region
        [SerializeField] Light light;
        [SerializeField] ApplyShaderToCamera applyShaderToCamera;//�O���b�`�G�t�F�N�g
        [SerializeField] Fisheye fishEye;//���჌���Y�G�t�F�N�g
        [SerializeField] Twirl twirl;//�Q�����G�t�F�N�g
        [SerializeField] Vortex vortex;//�Q�����G�t�F�N�g
        [SerializeField] AudioSource dollUuuFX;//�l�`�̂��߂���
        [SerializeField] AudioSource babyFX;//�Ԃ����̋�����
        [SerializeField] AudioSource ghostsGrumbleFX;//�H��̂��߂���
        [SerializeField] AudioSource ghostAh2FX;//�H��̔ߖ�
        [SerializeField] AudioSource mosquitoSound;//��̉�
        [SerializeField] AudioSource zowawa;//�]����
        [SerializeField] AudioSource ghostsGrumbleMan;//�H��̂��߂���(�j)
        [SerializeField] GameObject[] cockroaches;//�S�L�u��


        private string tagName = "VisibleTrg";
        #endregion



        private void Start()
        {

        }

        private void Update()
        {

        }

        void OwnDollEffectHandler()
        {

        }

        public override void PlayEffect()
        {

        }

        public void Scream()
        {

        }

        public void InsectSound()
        {

        }

        public void InsectsInfest()
        {
            for (int i = 0; i < cockroaches.Length; i++)
            {
                cockroaches[i].SetActive(true);
            }
        }

        public void Cry()
        {

        }

        public void Laugh()
        {

        }

        public void Tremble()
        {

        }

        public void LightFlicking()
        {

        }

        public void PoorVisibility(string trgName)
        {
            //�O���b�`�G�t�F�N�g�Ə΂��������o��
            if (trgName == "GlitchDollVisibleTrg")
            {
                applyShaderToCamera.enabled = true;
                dollUuuFX.Play();
            }
            else if (trgName == "FishEyeVisibleTrg")
            {

            }
            else if (trgName == "TwirlVisibleTrg")
            {

            }
            else if (trgName == "VortexVisibleTrg")
            {

            }
        }
        public void FootSound()
        {

        }

        public void LeadDoll()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            PoorVisibility(other.gameObject.name);
        }
    }
}



