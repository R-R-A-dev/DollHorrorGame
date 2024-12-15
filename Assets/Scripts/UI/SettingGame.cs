using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI 
{
    public class SettingGame : MonoBehaviour
    {
        bool gameSetting = false;
        [SerializeField] GameObject PauseMenu;
        [SerializeField] GameObject SettingScreen;
        void Start()
        {

        }

        void Update()
        {


        }

        public void SettingOpenButton()
        {
            if (!gameSetting)
            {
                gameSetting = true;
                PauseMenu.SetActive(false);
                SettingScreen.SetActive(true);
            }
        }

        public void SettingCloseButton() 
        {
            if (gameSetting)
            {
                gameSetting = false;
                PauseMenu.SetActive(true);
                SettingScreen.SetActive(false); 
            }
        }
    }
}

