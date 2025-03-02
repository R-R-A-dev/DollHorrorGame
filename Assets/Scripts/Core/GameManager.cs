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
 * タイトル画面からゲーム画面への移動
 * プロローグの開始からプレイヤーの操作開始までの流れ
 * ロード中にプレイヤーを動かして描画負荷を軽減させる
 * 
*/