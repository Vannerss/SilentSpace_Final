using System;
using SilentSpace.Core;
using SilentSpace.DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.UI
{
    public class GameSettings : MonoBehaviour
    {
        private readonly string[] _difficulties = new string[]
        {
            "easy", 
            "medium", 
            "hard", 
            "harder",
            "harderer",
            "hardest",
            "Impossible",
        };

        private PlayerManager _playerManager;
        private int _selectedDifficulty;
        private int _amountOfDifficultyInteractions;

        public TMP_Text difficultyLabel;
        public TextMeshProUGUI sensitivityLevel;
        public Slider sensitivitySlider;

        public event Action OnPassedDiffInteractions;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            
            if (PlayerPrefs.HasKey("SelectedDifficulty")) _selectedDifficulty = PlayerPrefs.GetInt("SelectedDifficulty");
            
            UpdateDifficultyLabel();
            
            if(_playerManager != null)
            {
                if (PlayerPrefs.HasKey("Sensitivity"))
                {
                    SetSensitiviy(PlayerPrefs.GetFloat("Sensitivity"));
                    sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
                }
            }
            else
            {
                SetSensitiviy(.3f);
            }

            this.gameObject.SetActive(false);
        }

        public void DiffLeft()
        {
            _selectedDifficulty--;
            if (_selectedDifficulty < 0)
            {
                _selectedDifficulty = _difficulties.Length - 1;
            }

            UpdateDifficultyLabel();
            
            _amountOfDifficultyInteractions++;
            if (_amountOfDifficultyInteractions == 6)
            {
                OnPassedDiffInteractions?.Invoke();
            }
            PlayerPrefs.SetInt("SelectedDifficulty", _selectedDifficulty);
        }      
        
        public void DiffRight()
        {
            _selectedDifficulty++;
            if (_selectedDifficulty > _difficulties.Length - 1)
            {
                _selectedDifficulty = 0;
            }
            
            UpdateDifficultyLabel();

            _amountOfDifficultyInteractions++;
            if (_amountOfDifficultyInteractions == 6)
            {
                OnPassedDiffInteractions?.Invoke();
            }
            PlayerPrefs.SetInt("SelectedDifficulty", _selectedDifficulty);
        }

        private void UpdateDifficultyLabel()
        {
            difficultyLabel.text = _difficulties[_selectedDifficulty];
        }

        public void SetSensitiviy(float value)
        {
            UpdateSensitivityLabel(value);
            if(_playerManager == null) return;
            _playerManager.xSensitivity = value;
            _playerManager.ySensitivity = value;
            PlayerPrefs.SetFloat("Sensitivity", value);
        }

        private void UpdateSensitivityLabel(float value)
        {
            sensitivityLevel.text = value.ToString("0.00");
        }

        public void Save()
        {
            DataPersistenceManager.Instance.SaveGame();
        }

        public void SaveAndQuit()
        {
            Application.Quit();
        }

        public void Load()
        {
            //Add load logic
        }
    }
}