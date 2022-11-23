using System;
using SilentSpace.Core;
using TMPro;
using UnityEngine;

namespace SilentSpace.UI
{
    public class GameplayUI : MonoBehaviour
    {
        private PlayerManager _playerManager;

        public TMP_Text healthLabel;
        public TMP_Text oxygenLabel;
        public TextMeshProUGUI percentageLabel;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }
        private void Update()
        {
            UpdateHealthLabel();
            UpdateOxygenLabel();
        }
        private void UpdateHealthLabel()
        {
            //var health = _playerManager.GetHp();
            if(_playerManager.health > 66)
            {
                healthLabel.color = new Color(.27f, 1f, 0, 1);
                percentageLabel.color = new Color(.27f, 1f, 0, 1);
            } 
            else if(_playerManager.health is <= 66f and > 33f)
            {
                healthLabel.color = new Color(.85f, .89f, 0, 1);
                percentageLabel.color = new Color(.85f, .89f, 0, 1);
            }
            else if(_playerManager.health <= 33f)
            {
                healthLabel.color = new Color(1, 0, 0, 255);
                percentageLabel.color = new Color(1, 0, 0, 255);
            }
            
            healthLabel.text = _playerManager.GetHp().ToString("0");
        }
        
        private void UpdateOxygenLabel()
        {
            oxygenLabel.text = _playerManager.GetOxygen().ToString("0");
        }
    }
}
