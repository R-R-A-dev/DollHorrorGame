using effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effect
{
    public class OtherDollsEffect : Effect
    {
        #region
        [SerializeField] ChildObject[] appearJpDoll;//出現する日本人形
        [SerializeField] ChildObject[] fallingJpDoll;//落下する日本人形

        private static int arrayNum0 = 0;
        private static int arrayNum1 = 1;
        private static int arrayNum2 = 2;
        private static int arrayNum3 = 3;


        #endregion


        void Start()
        {

        }

        void Update()
        {

        }

        public override void PlayEffect()
        {
            throw new System.NotImplementedException();
        }//TODO: 演出メソッドの記載

        /// <summary>
        /// 落下する日本人形
        /// </summary>
        public IEnumerator FallingJpDoll(string trgName, Transform rotate)
        {
            if (trgName == "TwirlVisibleTrg")
            {
                int AppearAngle = AppearAngleJudge(rotate);
                for (int i = 0; i < fallingJpDoll[AppearAngle].JpDollArray.Length; i++)
                {
                    fallingJpDoll[AppearAngle].JpDollArray[i].SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                }
            }

        }

        /// <summary>
        /// 日本人形の出現
        /// </summary>
        public IEnumerator AppearJpDoll(string trgName, Transform rotate)
        {
            if (trgName == "VortexVisibleTrg")
            {
                int AppearAngle = AppearAngleJudge(rotate);
                for (int i = 0; i < appearJpDoll[AppearAngle].JpDollArray.Length; i++)
                {
                    appearJpDoll[AppearAngle].JpDollArray[i].SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        int AppearAngleJudge(Transform rotate)
        {
            float angle = rotate.eulerAngles.y;
            //表示も確立
            if (90 > angle && angle > 0) return arrayNum0;
            else if (180 > angle && angle > 90) return arrayNum1;
            else if (270 > angle && angle > 180) return arrayNum2;
            else if (359.9 > angle && angle > 270) return arrayNum3;

            return 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            //怖くないので削除？
            //StartCoroutine(AppearJpDoll(other.gameObject.name, GetComponent<Transform>()));
            //StartCoroutine(FallingJpDoll(other.gameObject.name, GetComponent<Transform>()));
        }

        [System.Serializable]
        public class ChildObject
        {
            public GameObject[] JpDollArray;
        }
    }



}

