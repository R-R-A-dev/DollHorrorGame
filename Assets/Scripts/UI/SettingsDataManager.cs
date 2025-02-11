using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

namespace UI
{
    public class SettingsDataManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Slider brightnessSlider;
        [SerializeField] private Dropdown windowModeDropdown;
        [SerializeField] private Dropdown cameraShakeDropdown;
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Dropdown motionBlurDropdown;
        [SerializeField] private Dropdown antiAliasingDropdown;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider bgmVolumeSlider;
        [SerializeField] private Slider seVolumeSlider;
        [SerializeField] private Slider mouseSensitivitySlider;

        [Header("Graphics Value")]
        [SerializeField] private FirstPersonController fpsController;

        [Header("Control Value")]
        [SerializeField] private float mouseSensitivity;

        [Header("Audio Value")]
        [SerializeField] private AudioSource[] bgmSources; // BGM�̔z��
        [SerializeField] private AudioSource[] seSources;  // SE�̔z��

        [Header("Mouse")]
        [SerializeField] private MouseLook mouseLook;

        [Header("Post Processing")]
        [SerializeField] private PostProcessVolume postProcessVolume;
        private ColorGrading colorGrading;
        private MotionBlur motionBlur;

        private Resolution[] resolutions;
        private const string BrightnessKey = "Brightness";
        private const string WindowModeKey = "WindowMode";
        private const string CameraShakeKey = "CameraShake";
        private const string ResolutionKey = "Resolution";
        private const string MotionBlurKey = "MotionBlur";
        private const string AntiAliasingKey = "AntiAliasing";
        private const string MasterVolumeKey = "MasterVolume";
        private const string BGMVolumeKey = "BGMVolume";
        private const string SEVolumeKey = "SEVolume";
        private const string MouseSensitivityKey = "MouseSensitivity";

        void Start()
        {
            if (fpsController != null)
            {
                mouseLook = fpsController.GetMouseLook(); // MouseLook ���擾
            }

            SetupResolutionOptions();
            if (resolutions != null && resolutions.Length > 0)
            {
                LoadSettings();
            }
            else
            {
                Debug.LogError("Resolution initialization failed!");
            }


        }

        // �ݒ��ǂݍ���
        private void LoadSettings()
        {
            // ���邳�ݒ�
            if (postProcessVolume.profile.TryGetSettings(out colorGrading))
            {
                float brightness = PlayerPrefs.GetFloat(BrightnessKey, -1.5f);
                brightnessSlider.value = brightness;
                colorGrading.postExposure.value = brightness;
            }

            // �E�B���h�E���[�h
            int windowMode = PlayerPrefs.GetInt(WindowModeKey, 0);
            windowModeDropdown.value = windowMode;
            SetWindowMode(windowMode);

            // �J�����h��
            int cameraShake = PlayerPrefs.GetInt(CameraShakeKey, 0);
            cameraShakeDropdown.value = cameraShake;
            SetCameraShake(cameraShake);

            // �𑜓x
            int resolutionIndex = PlayerPrefs.GetInt(ResolutionKey, 0);
            resolutionDropdown.value = resolutionIndex;
            SetResolution(resolutionIndex);

            // ���[�V�����u���[
            if (postProcessVolume.profile.TryGetSettings(out motionBlur))
            {
                int motionBlur = PlayerPrefs.GetInt(MotionBlurKey, 0);
                motionBlurDropdown.value = motionBlur;
                SetMotionBlur(motionBlur);
            }


            // �A���`�G�C���A�V���O
            int antiAliasing = PlayerPrefs.GetInt(AntiAliasingKey, 0);
            antiAliasingDropdown.value = antiAliasing;
            SetAntiAliasing(antiAliasing);


            // �}�X�^�[����
            float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
            masterVolumeSlider.value = masterVolume;
            AudioListener.volume = masterVolume;

            // BGM����
            float bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 1f);
            bgmVolumeSlider.value = bgmVolume;
            SetBGMVolume(bgmSources, bgmVolume);

            // SE����
            float seVolume = PlayerPrefs.GetFloat(SEVolumeKey, 1f);
            seVolumeSlider.value = seVolume;
            SetSEVolume(seSources, seVolume);

            // �}�E�X���x
            float mouseSensitivity = PlayerPrefs.GetFloat(MouseSensitivityKey, 1f);
            mouseSensitivitySlider.value = mouseSensitivity;
            OnMouseSensitivityChanged(mouseSensitivity);
        }

        // **���邳�̕ύX**
        public void OnBrightnessChanged(float value)
        {
            if (colorGrading != null)
            {
                colorGrading.postExposure.value = value;
                PlayerPrefs.SetFloat(BrightnessKey, value);
                PlayerPrefs.Save();
            }
        }

        // **�E�B���h�E���[�h�̕ύX**
        public void OnWindowModeChanged(int index)
        {
            SetWindowMode(index);
            PlayerPrefs.SetInt(WindowModeKey, index);
            PlayerPrefs.Save();
        }

        private void SetWindowMode(int mode)
        {
            switch (mode)
            {
                case 0: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; Debug.Log("[WindowMode] Set to: Fullscreen Window"); break;
                case 1: Screen.fullScreenMode = FullScreenMode.Windowed; Debug.Log("[WindowMode] Set to: Windowed"); break;
            }
        }

        // **�J�����h��̕ύX**
        public void OnCameraShakeChanged(int index)
        {
            SetCameraShake(index);
            PlayerPrefs.SetInt(CameraShakeKey, index);
            PlayerPrefs.Save();
        }

        private void SetCameraShake(int index)
        {
            if (index == 0)
            {
                FirstPersonController.stopHeadBob = true;
            }
            else if (index == 1)
            {
                FirstPersonController.stopHeadBob = false;
            }
        }

        // **�𑜓x�̕ύX**
        public void OnResolutionChanged(int index)
        {
            SetResolution(index);
            PlayerPrefs.SetInt(ResolutionKey, index);
            PlayerPrefs.Save();
        }

        private void SetResolution(int index)
        {
            if (resolutions == null || resolutions.Length == 0)
            {
                Debug.LogError("Resolutions array is not initialized!");
                return;
            }

            if (index < 0 || index >= resolutions.Length)
            {
                Debug.LogError($"Resolution index {index} is out of range!");
                return;
            }

            Resolution resolution = resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            Debug.Log($"[Resolution] Set to: {resolution.width}x{resolution.height} FullScreen: {Screen.fullScreen}");
        }

        // **���[�V�����u���[�̕ύX**
        public void OnMotionBlurChanged(int index)
        {
            SetMotionBlur(index);
            PlayerPrefs.SetInt(MotionBlurKey, index);
            PlayerPrefs.Save();
        }

        private void SetMotionBlur(int index)
        {
            if (index == 0)
            {
                motionBlur.active = true;
            }
            else if (index == 1)
            {
                motionBlur.active = false;
            }
        }

        // **�A���`�G�C���A�V���O�̕ύX**
        public void OnAntiAliasingChanged(int index)
        {
            SetAntiAliasing(index);
            PlayerPrefs.SetInt(AntiAliasingKey, index);
            PlayerPrefs.Save();
        }

        private void SetAntiAliasing(int index)
        {

            if (index == 0)
            {
                QualitySettings.antiAliasing = index;
            }
            else if (index == 1)
            {
                QualitySettings.antiAliasing = index;
            }
        }

        // **���ʕύX**
        public void OnMasterVolumeChanged(float value)
        {
            AudioListener.volume = value;
            PlayerPrefs.SetFloat(MasterVolumeKey, value);
            PlayerPrefs.Save();
        }

        public void OnBGMVolumeChanged(float value)
        {
            SetBGMVolume(bgmSources, value);
            PlayerPrefs.SetFloat(BGMVolumeKey, value);
            PlayerPrefs.Save();
        }

        private void SetBGMVolume(AudioSource[] BGMSources, float value)
        {
            foreach (var bgmSources in BGMSources)
            {
                if (bgmSources != null)
                {
                    bgmSources.volume = value;
                }
            }

        }

        public void OnSEVolumeChanged(float value)
        {
            SetSEVolume(seSources, value);
            PlayerPrefs.SetFloat(SEVolumeKey, value);
            PlayerPrefs.Save();
        }

        private void SetSEVolume(AudioSource[] SESources, float value)
        {
            foreach (var SESource in SESources)
            {
                if (SESource != null)
                {
                    SESource.volume = value;
                }
            }
        }

        // **�}�E�X���x�̕ύX**
        public void OnMouseSensitivityChanged(float value)
        {
            if (mouseLook == null)
            {
                Debug.LogError("MouseLook is not assigned!");
                return;
            }

            mouseLook.XSensitivity = value;
            mouseLook.YSensitivity = value;
            PlayerPrefs.SetFloat(MouseSensitivityKey, value);
            PlayerPrefs.Save();
        }

        // **�𑜓x�̑I�������Z�b�g�A�b�v**
        private void SetupResolutionOptions()
        {
            resolutions = Screen.resolutions;
            if (resolutions == null || resolutions.Length == 0)
            {
                Debug.LogError("No available screen resolutions found!");
                return;
            }
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
            }

            resolutionDropdown.AddOptions(options);
        }
    }
}
