using UnityEngine.SceneManagement;
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
        private PlayerManager _playerManager;
        
        public GameObject journalUI;
        public GameObject objectivesUI;
        public GameObject noteSelectionUI;
        public GameObject gamePlayUI;
        public GameObject settingsUI;
        public GameObject mapUI;
        public GameObject winUI;
        public GameObject deathUI;
        public GameObject noteUI;

        public int CurrentOBJ = 0;
        //public GameObject listaUI;
        

        private void Start()
        {
            _audioController = AudioController.Instance;
            
            _inputManager = InputManager.Instance;
            _inputManager.OnPause += SettingsMenu;
            _inputManager.OnJournal += JournalMenu;
            _inputManager.OnMap += MapMenu;

            _onStart = false;
            
            _playerManager = PlayerManager.Instance;
            _playerManager.OnPlayerDeath += OpenDeathMenu;
            
            journalUI.SetActive(false);
            noteSelectionUI.SetActive(false);
            objectivesUI.SetActive(false);
            noteUI.SetActive(false);
            //listaUI.SetActive(false);


        }
        
        public void SettingsMenu()
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

        public void JournalMenu()
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
            noteSelectionUI.SetActive(false);
            noteUI.SetActive(false);
            //listaUI.SetActive(false);
        }
        
        public void OpenNotesUI()
        {
            
            noteSelectionUI.SetActive(true);
            journalUI.SetActive(true);
            objectivesUI.SetActive(true);
            noteUI.SetActive(false);
            //listaUI.SetActive(false);
            
        }

        public void OpenNoteUI()
        {
            noteUI.SetActive(true);
            journalUI.SetActive(true);
            objectivesUI.SetActive(true);
            noteSelectionUI.SetActive(false);
            //listaUI.SetActive(false);
        }

        
        public void OpenObjectivesUI()
        {
            
            //listaUI.SetActive(true);
            noteSelectionUI.SetActive(false);
            journalUI.SetActive(true);
            objectivesUI.SetActive(true);
            noteUI.SetActive(false);
        }
        
        
        public void MapMenu()
        {
            if (settingsUI.activeSelf) return;
            if (!mapUI.activeSelf)
            {
                _inputManager.DisableLookInputs();
                _inputManager.DisableMovementInputs();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mapUI.SetActive(true);
                return;
            }
            _inputManager.EnableLookInputs();
            _inputManager.EnableMovementInputs();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mapUI.SetActive(false);
        }

        public void OpenDeathMenu()
        {
            _inputManager.DisableMovementInputs();
            _inputManager.DisableLookInputs();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            deathUI.SetActive(true);
        }

        public void OpenWinMenu()
        {
            _inputManager.DisableMovementInputs();
            _inputManager.DisableLookInputs();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            winUI.SetActive(true);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }

        #region UI Audio references

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

        #endregion
    }
}
