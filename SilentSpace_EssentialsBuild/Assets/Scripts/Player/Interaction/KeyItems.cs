using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class KeyItems : MonoBehaviour, IInteractable
    {
        private PlayerManager _playerManager;

        public string InteractionPrompt { get; }
        public string itemName;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        public void Interact()
        {
            if (!_playerManager.InventoryHas(itemName))
            {
                _playerManager.AddToInventory(itemName);
            }
            
            gameObject.SetActive(false);
        }
    }
}
