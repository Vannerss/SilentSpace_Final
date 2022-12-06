using System;
using SilentSpace.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.UI
{
    public class GameplayUI : MonoBehaviour
    {
        private PlayerManager _playerManager;

        public Image healthLabel;
        public Image oxygenLabel;
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
            // if(_playerManager.health > 66)
            // {
            //     healthLabel.color = new Color(.27f, 1f, 0, 1);
            //     percentageLabel.color = new Color(.27f, 1f, 0, 1);
            // } 
            // else if(_playerManager.health is <= 66f and > 33f)
            // {
            //     healthLabel.color = new Color(.85f, .89f, 0, 1);
            //     percentageLabel.color = new Color(.85f, .89f, 0, 1);
            // }
            // else if(_playerManager.health <= 33f)
            // {
            //     healthLabel.color = new Color(1, 0, 0, 255);
            //     percentageLabel.color = new Color(1, 0, 0, 255);
            // }

            healthLabel.fillAmount = _playerManager.GetHp() / 100;
        }
        
        private void UpdateOxygenLabel()
        {
            oxygenLabel.fillAmount = _playerManager.GetOxygen() / _playerManager.maxOxygenLevel;
        }
    }
}
