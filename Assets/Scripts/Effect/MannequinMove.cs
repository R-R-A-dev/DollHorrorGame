using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effect.mannequin
{
    public class MannequinMove : MonoBehaviour
    {
        [SerializeField] ChildObject[] AppearMannequins;//マネキンの出現場所
        [SerializeField] Flashlight flashlight;//懐中電灯
        [SerializeField] Light light;
        [SerializeField] ChildObject[] FallingMannequins;//落下マネキン
        [SerializeField] ChildObject[] FallingAppearMannequins;//落下マネキン以外の出現
        
        private string[] appearTrgName;//トリガー名
        private static int arrayNum0 = 0;
        private static int arrayNum1 = 1;
        private static int arrayNum2 = 2;
        private static int arrayNum3 = 3;
        private int AppearAngle;//出現するマネキンの角度

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
        /// 演出 マネキンの出現場所 懐中電灯の明滅からの目前に出現
        /// </summary>
        /// <param name="trgName"></param>
        void Appear(string trgName)
        {
            if (trgName == appearTrgName[arrayNum0])
            {
                //懐中電灯の明滅
                //StartCoroutine(LightFlicking(AppearPos));
            }
        }

        /// <summary>
        /// 懐中電灯の明滅演出　マネキンの出現
        /// </summary>
        IEnumerator LightFlickingAppear(int AppearPos)
        {
            //  ライト明滅マネキン出現
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
        /// マネキンの落下演出
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
        /// 角度によって出現させるマネキンの位置を変える
        /// </summary>
        int AppearAngleJudge(Transform rotate)
        {
            float angle = rotate.eulerAngles.y;
            //表示も確立
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
/*追いかけられる→マネキンの出現→止まる→一定の距離で消える→正面で出現襲う
 *写真演出
 * 
*/