using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        public bool paused = false;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] FirstPersonController ThePlayer;
        [SerializeField] MouseLook mouseLook;

        public void pause()
        {
            if (!paused)
            {
                Time.timeScale = 0;
                ThePlayer.enabled = false;
                paused = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                paused = false;
                Time.timeScale = 1;
            }

        }

        public void RestartGameButton()
        {
            Time.timeScale = 1;
            ThePlayer.enabled = true;
            pauseMenu.SetActive(false);
            paused = false;
        }
    }
}



