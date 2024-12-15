using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        public bool gamePaused = false;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] FirstPersonController ThePlayer;


        public void pause()
        {
            if (!gamePaused)
            {
                Time.timeScale = 0;
                ThePlayer.enabled = false;
                gamePaused = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                ThePlayer.enabled = true;
                gamePaused = false;
                Time.timeScale = 1;
            }

        }

        public void RestartGameButton()
        {
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            ThePlayer.enabled = true;
            gamePaused = false;
            Time.timeScale = 1;
        }
        public void enVisible()
        {
            Cursor.visible = false;
        }
    }
}



