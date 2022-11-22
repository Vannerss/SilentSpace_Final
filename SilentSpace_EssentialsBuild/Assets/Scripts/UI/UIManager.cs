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
        private bool _onStart = true;
        
        public GameObject journalUI;
        public GameObject objectivesUI;
        public GameObject notesUI;
        public GameObject gamePlayUI;
        public GameObject settingsUI;

        private void Start()
        {
            _audioController = AudioController.Instance;
            
            _inputManager = InputManager.Instance;
            _inputManager.OnOpenPause += OpenSettingsMenu;
            _inputManager.OnOpenJournal += OpenJournalMenu;

            _onStart = false;
        }

        public void OpenSettingsMenu()
        {
            if (!settingsUI.activeSelf)
            {
                Debug.Log("Open", this);
                gamePlayUI.SetActive(false);
                settingsUI.SetActive(true);
            } 
            else
            {
                Debug.Log("Close", this);
                gamePlayUI.SetActive(true);
                settingsUI.SetActive(false);
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
