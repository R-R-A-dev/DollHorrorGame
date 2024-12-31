using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effect.mannequin
{
    public class MannequinMove : MonoBehaviour
    {
        //出現エフェクト
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
        /// 演出 マネキンの出現場所 飼い中電との明滅からの目前に出現
        /// </summary>
        /// <param name="trgName"></param>
        public IEnumerator Appear(string trgName)
        {
            //TODO: 実装　方向ごとの出現場所と演出の内容
            //内容の記述

            if (trgName == AppearTrgName[0])
            {
                //懐中電灯の明滅
                flashlight.LightFlicking();
                //プレイヤーの向いている角度からマネキンの出現
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
            /*実装に必要な物
             * トリガー
             * マネキンの場所
             * 飼い中電との明滅
             * 
             * 流れ
             * トリガー踏む　トリガーの場所によって登場場所を変える
             * 懐中電灯明滅と同時にマネキン出現
             * 
            */
        }
        //演出を複数持たせる

        /// <summary>
        /// 角度によって出現させるマネキンの位置を変える
        /// </summary>
        public void AppearAngleJudge(Transform rotate)
        {
            //表示も確立

        }

    }

    [System.Serializable]
    public class ChildObject
    {
        public GameObject[] MannequinArray;
    }

}

