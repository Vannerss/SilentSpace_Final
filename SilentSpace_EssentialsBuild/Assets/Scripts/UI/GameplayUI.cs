using SilentSpace.Core;
using TMPro;
using UnityEngine;

namespace SilentSpace.UI
{
    public class GameplayUI : MonoBehaviour
    {
        private PlayerManager _playerManager;

        public TextMeshProUGUI healthLabel;
        public TextMeshProUGUI percentageLabel;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }
        private void Update()
        {
            UpdateHealthLabel();
        }
        private void UpdateHealthLabel()
        {
            var health = _playerManager.GetHp();
            if(health > 66)
            {
                healthLabel.color = new Color(.27f, 1f, 0, 1);
                percentageLabel.color = new Color(.27f, 1f, 0, 1);
            } 
            else if(health <= 66f && health > 33f)
            {
                healthLabel.color = new Color(.85f, .89f, 0, 1);
                percentageLabel.color = new Color(.85f, .89f, 0, 1);
            }
            else if(health <= 33f)
            {
                healthLabel.color = new Color(1, 0, 0, 255);
                percentageLabel.color = new Color(1, 0, 0, 255);
            }
            healthLabel.text = health.ToString();
        }
    }
}
