using System;
using UnityEngine;
using TMPro;

namespace SilentSpace.UI
{
    public class SettingsUI : MonoBehaviour
    {
        private GameSettings _gameSettingsScript;

        public bool onMainMenu;
        public TMP_Text currentMenu;
        public GameObject bg;
        public GameObject gameSettingsOptions;
        public GameObject videoSettingsOptions;
        public GameObject audioSettingsOptions;
        public GameObject difficultyInteractionMessageScreen;

        private void OnEnable()
        {
            gameSettingsOptions.SetActive(true);
            bg.SetActive(true);
            currentMenu.text = "GAME SETTINGS";
            
            if(onMainMenu) return;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            bg.SetActive(false);
            videoSettingsOptions.SetActive(false);
            audioSettingsOptions.SetActive(false);
            gameSettingsOptions.SetActive(false);
            
            if(onMainMenu) return;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }

        private void Start()
        {
            _gameSettingsScript = gameSettingsOptions.GetComponent<GameSettings>();
            _gameSettingsScript.OnPassedDiffInteractions += OpenDiffMessage;
        }

        private void OpenDiffMessage()
        {
            difficultyInteractionMessageScreen.SetActive(true);
            _gameSettingsScript.OnPassedDiffInteractions -= OpenDiffMessage;
        }

        public void CloseDiffMessage()
        {
            difficultyInteractionMessageScreen.SetActive(false);
        }
        
        public void OpenAudioSettings()
        {
            audioSettingsOptions.SetActive(true);
            videoSettingsOptions.SetActive(false);
            gameSettingsOptions.SetActive(false);
            currentMenu.text = "AUDIO SETTINGS";
        }

        public void OpenVideoSettings()
        {
            videoSettingsOptions.SetActive(true);
            audioSettingsOptions.SetActive(false);
            gameSettingsOptions.SetActive(false);
            currentMenu.text = "VIDEO SETTINGS";
        }
        public void OpenGameSettings()
        {
            gameSettingsOptions.SetActive(true);
            videoSettingsOptions.SetActive(false);
            audioSettingsOptions.SetActive(false);
            currentMenu.text = "GAME SETTINGS";
        }
    }
}
