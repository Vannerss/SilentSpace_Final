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
            healthLabel.fillAmount = _playerManager.GetHp() / 100;
        }
        
        private void UpdateOxygenLabel()
        {
            oxygenLabel.fillAmount = _playerManager.GetOxygen() / 100;
        }
    }
}
