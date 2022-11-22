using System;
using SilentSpace.Audio;
using UnityEngine;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.UI.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        private AudioController _audioController;
        private bool _onStart = true;
        
        public GameObject settingsUI;
        public GameObject titleMenu;

        private void Start()
        {
            _audioController = AudioController.Instance;
        }

        public void OpenSettingsMenu()
        {
            _onStart = false;
            if (!settingsUI.activeSelf)
            {
                Debug.Log("Open", this);
                settingsUI.SetActive(true);
                titleMenu.SetActive(false);
            } 
            else
            {
                Debug.Log("Close", this);
                settingsUI.SetActive(false);
                titleMenu.SetActive(true);
            }
        }

        public void Click11()
        {
            if (_onStart) return;
            _audioController.PlayAudio(AudioType.SFX_UI_Button11_Button);
        }

        public void Click28()
        {
            if (_onStart) return;
            _audioController.PlayAudio(AudioType.SFX_UI_Button29_Button);
        }

        public void Clack()
        {
            if (_onStart) return;
            _audioController.PlayAudio(AudioType.SFX_UI_Clack_Slider);
        }

        public void SpaceyClick()
        {
            if (_onStart) return;
            _audioController.PlayAudio(AudioType.SFX_UI_SpaceyClick_Button);
        }
    }
}
