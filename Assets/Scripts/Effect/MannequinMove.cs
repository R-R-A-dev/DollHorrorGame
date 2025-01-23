using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effect.mannequin
{
    public class MannequinMove : MonoBehaviour
    {
        [SerializeField] ChildObject[] AppearMannequins;//�}�l�L���̏o���ꏊ
        [SerializeField] Flashlight flashlight;//�����d��
        [SerializeField] Light light;
        [SerializeField] ChildObject[] FallingMannequins;//�����}�l�L��
        [SerializeField] ChildObject[] FallingAppearMannequins;//�����}�l�L���ȊO�̏o��
        
        private string[] appearTrgName;//�g���K�[��
        private static int arrayNum0 = 0;
        private static int arrayNum1 = 1;
        private static int arrayNum2 = 2;
        private static int arrayNum3 = 3;
        private int AppearAngle;//�o������}�l�L���̊p�x

        private void Start()
        {
            appearTrgName = new string[]
            { "MannequinAppearTrg", "MannequinFallingTrg", "AppearTrg3",
                };
        }

        public void MoveEffectHandler(string trgName,Transform rotate)
        {
            AppearAngle = AppearAngleJudge(rotate);
            Appear(trgName);
            FallingMannequin(trgName);

        }
        /// <summary>
        /// ���o �}�l�L���̏o���ꏊ �����d���̖��ł���̖ڑO�ɏo��
        /// </summary>
        /// <param name="trgName"></param>
        void Appear(string trgName)
        {
            if (trgName == appearTrgName[arrayNum0])
            {
                //�����d���̖���
                //StartCoroutine(LightFlicking(AppearPos));
            }
        }

        /// <summary>
        /// �����d���̖��ŉ��o�@�}�l�L���̏o��
        /// </summary>
        IEnumerator LightFlickingAppear(int AppearPos)
        {
            //  ���C�g���Ń}�l�L���o��
            light.enabled = false;
            yield return new WaitForSeconds(0.05f);
            light.enabled = true;
            AppearMannequins[AppearPos].MannequinArray[arrayNum0].SetActive(true);
            yield return new WaitForSeconds(0.05f);
            light.enabled = false;
            yield return new WaitForSeconds(0.05f);
            light.enabled = true;
            yield return new WaitForSeconds(0.05f);
            light.enabled = false;
            AppearMannequins[AppearPos].MannequinArray[arrayNum0].SetActive(false);
            yield return new WaitForSeconds(0.05f);
            light.enabled = true;
            AppearMannequins[AppearPos].MannequinArray[arrayNum1].SetActive(true);
        }

        /// <summary>
        /// �}�l�L���̗������o
        /// </summary>
        /// <param name="trgName"></param>
        void FallingMannequin(string trgName)
        {
            if (trgName == appearTrgName[arrayNum1])
            {
                for (int i = 0; i < FallingMannequins[AppearAngle].MannequinArray.Length; i++)
                {
                    FallingMannequins[AppearAngle].MannequinArray[i].SetActive(true);
                }
                for (int i = 0; i < FallingAppearMannequins[AppearAngle].MannequinArray.Length; i++)
                {
                    FallingAppearMannequins[AppearAngle].MannequinArray[i].SetActive(true);
                }
            }
        }


        /// <summary>
        /// �p�x�ɂ���ďo��������}�l�L���̈ʒu��ς���
        /// </summary>
        int AppearAngleJudge(Transform rotate)
        {
            float angle = rotate.eulerAngles.y;
            //�\�����m��
            if (90> angle && angle > 0) return arrayNum0;
            else if (180> angle && angle > 90) return arrayNum1;
            else if (270> angle && angle > 180) return arrayNum2;
            else if (359.9> angle && angle>270) return arrayNum3;

            return 0;
        }



        [System.Serializable]
        public class ChildObject
        {
            public GameObject[] MannequinArray;
        }

    }
}
/*�ǂ��������遨�}�l�L���̏o�����~�܂遨���̋����ŏ����遨���ʂŏo���P��
 *�ʐ^���o
 * 
*/