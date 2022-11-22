using SilentSpace.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.Interactables
{
    public class Pickables : MonoBehaviour
    {
        [SerializeField] private Button itemButton;
        
        private InputManager _inputManager;
        private PlayerManager _playerManager;
        public TMP_Text text;

        public ItemTypes item;
        
        private void Start()
        {
            #region Manager Instances
            if (InputManager.Instance != null)
            {
                _inputManager = InputManager.Instance;
            }
            else
            {
                Debug.LogWarning("[Pickables.cs]: The Input Manager instance is null");
            }

            if (PlayerManager.Instance != null)
            {
                _playerManager = PlayerManager.Instance;
            }
            else
            {
                Debug.LogWarning("[Pickables.cs]: The Player Manager instance is null");
            }
            #endregion
        }

        private void OnMouseEnter()
        {
            _inputManager.OnInteractStarted += PickUp;
            //TODO: make ui dot increase on size
        }

        private void OnMouseExit()
        {
            _inputManager.OnInteractStarted -= PickUp;
            //TODO: make ui dot decrease on size
        }

        private void PickUp()
        {
            _inputManager.OnInteractStarted -= PickUp;
            _playerManager.PickedUpItems++;
            itemButton.interactable = item == ItemTypes.Note;
            Destroy(this.gameObject);
        }
    }
}
