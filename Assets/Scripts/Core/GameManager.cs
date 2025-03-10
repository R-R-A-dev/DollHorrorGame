using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

namespace Core
    {
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Prologue prologue;

        public static GameManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null) instance = this;
               else Destroy(gameObject);
            FirstPersonController.freezeTrg = true;
        }

        void Start()
        {
            //タイトル画面からゲーム画面へのシーン移動



            //プロローグの開始
            StartCoroutine(prologue.StartPrologue());
            //
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
/*
 * 演出中に音の変更
 * フォントの変更
*/