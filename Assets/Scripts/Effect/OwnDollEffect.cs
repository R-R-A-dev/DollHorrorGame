using System.Collections;
using System.Collections.Generic;
using TriangleNet.Topology.DCEL;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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
        [SerializeField] GameObject fpsController;//�v���C���[
        [SerializeField] Transform target;//�������
        [SerializeField] GameObject wall;//�������
        [SerializeField] PostProcessVolume volume;


        public float fishEyeDuration = 3f;
        private float fishEyeElapsedTimeFisheye = 0f;
        private bool isFishEye = false;

        private float elapsedTime = 0f;
        private bool isMoving = false;
        private Vector3 startPos;
        private Vector3 endPos;
        float duration = 0.7f;

        public float twirlDuration = 10f;
        private float twirlElapsedTime = 0f;
        private bool isTwirl = false;

        private float vortexDuration = 10f;
        private float vortexElapsedTime = 0f;
        private bool isVortex= false;
        #endregion



        private void Start()
        {

        }

        private void Update()
        {
            if (isMoving)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                transform.position = Vector3.Lerp(transform.position, target.position, t);

                if (t >= 1.0f)
                {
                    isMoving = false;
                }
            }

            if (isFishEye)
            {
                fishEyeElapsedTimeFisheye += Time.deltaTime;
                if (fishEyeElapsedTimeFisheye <= fishEyeDuration)
                {
                    float t = fishEyeElapsedTimeFisheye / fishEyeDuration;
                    fishEye.strengthX = Mathf.Lerp(0f, 1.5f, t);
                    fishEye.strengthY = Mathf.Lerp(0f, 1f, t);
                }
            }

            if (isVortex)
            {
                vortexElapsedTime += Time.deltaTime;
                if (vortexElapsedTime <= vortexDuration)
                {
                    float t = vortexElapsedTime / vortexDuration;
                    vortex.angle = Mathf.Lerp(0f, 360f, t);
                }
            }

            if (isTwirl)
            {
                twirlElapsedTime += Time.deltaTime;
                if (twirlElapsedTime <= twirlDuration)
                {
                    float t = twirlElapsedTime / twirlDuration;
                    twirl.angle = Mathf.Lerp(0f, 360f, t);
                }
            }
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

        public IEnumerator PoorVisibility(GameObject trg)
        {
            string trgName = trg.name;
            //�O���b�`�G�t�F�N�g�Ə΂��������o��
            if (trgName == "GlitchDollVisibleTrg")
            {
                trg.SetActive(false);
                applyShaderToCamera.enabled = true;
                ghostsGrumbleFX.Play();
                wall.SetActive(true);
                yield return new WaitForSeconds(20f);
                applyShaderToCamera.enabled = false;
                ghostsGrumbleFX.Stop();
                wall.SetActive(false);
                
            }
            else if (trgName == "FishEyeVisibleTrg")
            {
                trg.SetActive(false);
                isFishEye = true;
                yield return new WaitForSeconds(1f);
                babyFX.Play();
                yield return new WaitForSeconds(9f);
                isFishEye = false;
                babyFX.Stop();
                fishEye.strengthY = 0f;
                fishEye.strengthX = 0f;
                
            }
            else if (trgName == "TwirlVisibleTrg")
            {
                isTwirl = true;
                trg.SetActive(false);
                yield return new WaitForSeconds(10f);
                isTwirl = false;
            }
            else if (trgName == "VortexVisibleTrg")
            {
                isVortex = true;
                trg.SetActive(false);
                yield return new WaitForSeconds(10f);
                isVortex = false;
                vortex.enabled = false;
            }
            
        }
        public void FootSound()
        {

        }

        public void LeadCall()
        {

        }
        public void StartMove()
        {
            startPos = transform.position;
            endPos = target.position;
            elapsedTime = 0f;
            isMoving = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(PoorVisibility(other.gameObject));
        }
    }
}



