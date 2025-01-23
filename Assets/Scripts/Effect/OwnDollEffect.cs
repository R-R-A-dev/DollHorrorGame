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
        [SerializeField] ApplyShaderToCamera applyShaderToCamera;//グリッチエフェクト
        [SerializeField] Fisheye fishEye;//魚眼レンズエフェクト
        [SerializeField] Twirl twirl;//渦巻きエフェクト
        [SerializeField] Vortex vortex;//渦巻きエフェクト
        [SerializeField] AudioSource dollUuuFX;//人形のうめき声
        [SerializeField] AudioSource babyFX;//赤ちゃんの泣き声
        [SerializeField] AudioSource ghostsGrumbleFX;//幽霊のうめき声
        [SerializeField] AudioSource ghostAh2FX;//幽霊の悲鳴
        [SerializeField] AudioSource mosquitoSound;//蚊の音
        [SerializeField] AudioSource zowawa;//ゾワワ
        [SerializeField] AudioSource ghostsGrumbleMan;//幽霊のうめき声(男)
        [SerializeField] GameObject[] cockroaches;//ゴキブリ


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
            //グリッチエフェクトと笑い声複数出現
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



