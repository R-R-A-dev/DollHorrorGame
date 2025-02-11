using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI 
{
    public class SettingUIManager : MonoBehaviour
    {
        bool gameSetting = false;
        [SerializeField] GameObject PauseMenu;
        [SerializeField] GameObject SettingScreen;
        [SerializeField] GameObject GraphicsTab;
        [SerializeField] GameObject GraphicArea;
        [SerializeField] GameObject AudioTab;
        [SerializeField] GameObject AudioArea;
        [SerializeField] GameObject ControlTab;
        [SerializeField] GameObject ControlArea;

        public void GraphicsAreaTab()
        {
            GraphicArea.SetActive(true);
            AudioArea.SetActive(false);
            ControlArea.SetActive(false);
        }

        public void AudioAreaTab()
        {
            GraphicArea.SetActive(false);
            AudioArea.SetActive(true);
            ControlArea.SetActive(false);
        }

        public void ControlAreaTab()
        {
            GraphicArea.SetActive(false);
            AudioArea.SetActive(false);
            ControlArea.SetActive(true);
        }

        public void SettingOpenButton()
        {
            if (!gameSetting)
            {
                gameSetting = true;
                PauseMenu.SetActive(false);
                SettingScreen.SetActive(true);
                GraphicArea.SetActive(true);
            }
        }

        public void SettingCloseButton() 
        {
            if (gameSetting)
            {
                gameSetting = false;
                PauseMenu.SetActive(true);
                SettingScreen.SetActive(false);
                GraphicArea.SetActive(false);
                AudioArea.SetActive(false);
                ControlArea.SetActive(false);
            }
        }
    }
}

