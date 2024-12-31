using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effect.mannequin
{
    public class MannequinMove : MonoBehaviour
    {
        //�o���G�t�F�N�g
        [SerializeField] GameObject AppearTrg;
        [SerializeField] ChildObject[] AppearMannequins;
        [SerializeField] Light light;
        [SerializeField] Flashlight flashlight;
        private string[] AppearTrgName;

        private void Start()
        {
            AppearTrgName = new string[]
            { "MannequinAppearTrg", "AppearTrg2", "AppearTrg3",
                };
        }

        public void MoveEffectHandler(string trgName)
        {
            StartCoroutine(Appear(trgName));
        }
        /// <summary>
        /// ���o �}�l�L���̏o���ꏊ �������d�Ƃ̖��ł���̖ڑO�ɏo��
        /// </summary>
        /// <param name="trgName"></param>
        public IEnumerator Appear(string trgName)
        {
            //TODO: �����@�������Ƃ̏o���ꏊ�Ɖ��o�̓��e
            //���e�̋L�q

            if (trgName == AppearTrgName[0])
            {
                //�����d���̖���
                flashlight.LightFlicking();
                //�v���C���[�̌����Ă���p�x����}�l�L���̏o��
                //AppearAngleJudge();


                light.enabled = false;
                yield return new WaitForSeconds(0.2f);
                light.enabled = true;
                yield return new WaitForSeconds(0.3f);
                light.enabled = false;
                yield return new WaitForSeconds(0.2f);
                light.enabled = true;
                AppearMannequins[0].MannequinArray[0].SetActive(true);

            }
            else if (trgName == AppearTrgName[1])
            {
                Debug.Log(trgName);
            }
            /*�����ɕK�v�ȕ�
             * �g���K�[
             * �}�l�L���̏ꏊ
             * �������d�Ƃ̖���
             * 
             * ����
             * �g���K�[���ށ@�g���K�[�̏ꏊ�ɂ���ēo��ꏊ��ς���
             * �����d�����łƓ����Ƀ}�l�L���o��
             * 
            */
        }
        //���o�𕡐���������

        /// <summary>
        /// �p�x�ɂ���ďo��������}�l�L���̈ʒu��ς���
        /// </summary>
        public void AppearAngleJudge(Transform rotate)
        {
            //�\�����m��

        }

    }

    [System.Serializable]
    public class ChildObject
    {
        public GameObject[] MannequinArray;
    }

}

