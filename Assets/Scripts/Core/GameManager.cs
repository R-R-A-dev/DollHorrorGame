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
            //�^�C�g����ʂ���Q�[����ʂւ̃V�[���ړ�



            //�v�����[�O�̊J�n
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
 * ���o���ɉ��̕ύX
 * �t�H���g�̕ύX
*/