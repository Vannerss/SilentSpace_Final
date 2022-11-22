using SilentSpace.Audio;
using SilentSpace.Core;
using TMPro;
using UnityEngine;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.UI
{
    public class UIManager : MonoBehaviour
    {
        private InputManager _inputManager;
        private AudioController _audioController;
        private GameSettings _gameSettingsScript;

        public bool onMainMenu;
        public TMP_Text currentMenu;
        public GameObject journalUI;
        public GameObject objectivesUI;
        public GameObject notesUI;
        public GameObject gamePlayUI;
        public GameObject background;
        public GameObject settingsUI;
        public GameObject gameSettingsOptions;
        public GameObject videoSettingsOptions;
        public GameObject audioSettingsOptions;
        public GameObject difficultyInteractionMessageScreen;

        private void Awake()
        {
            videoSettingsOptions.SetActive(true);
            audioSettingsOptions.SetActive(true);
            gameSettingsOptions.SetActive(true);
        }

        private void Start()
        {
            _audioController = AudioController.Instance;

            _gameSettingsScript = gameSettingsOptions.GetComponent<GameSettings>();
            _gameSettingsScript.OnPassedDiffInteractions += OpenDiffMessage;

            if(onMainMenu) return;
            _inputManager = InputManager.Instance;
            _inputManager.OnOpenPause += OpenSettingsMenu;
            _inputManager.OnOpenJournal += OpenJournalMenu;
            
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
        public void OpenSettingsMenu()
        {
            if (!settingsUI.activeSelf)
            {
                Debug.Log("Open", this);
                if(gamePlayUI != null) gamePlayUI.SetActive(false);
                background.SetActive(true);
                settingsUI.SetActive(true);
                gameSettingsOptions.SetActive(true);
                currentMenu.text = "GAME SETTINGS";
                if(onMainMenu) return;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;

            } 
            else
            {
                Debug.Log("Close", this);
                if(gamePlayUI != null) gamePlayUI.SetActive(true);
                background.SetActive(false);
                settingsUI.SetActive(false);
                videoSettingsOptions.SetActive(false);
                audioSettingsOptions.SetActive(false);
                gameSettingsOptions.SetActive(false);
                if(onMainMenu) return;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }

        private void OpenJournalMenu()
        {
            if (settingsUI.activeSelf) return;
            
            if (!journalUI.activeSelf)
            {
                _inputManager.DisableLookInputs();
                _inputManager.DisableMovementInputs();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                journalUI.SetActive(true);
                objectivesUI.SetActive(true);
                return;
            }
            _inputManager.EnableLookInputs();
            _inputManager.EnableMovementInputs();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            journalUI.SetActive(false);
            objectivesUI.SetActive(false);
            notesUI.SetActive(false);
        }

        public void PlayClick()
        {
            _audioController.PlayAudio(AudioType.SFX_UI_SpaceyClick_Button);
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
