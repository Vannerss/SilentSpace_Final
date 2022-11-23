using System;
using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Interactables
{
    public class EndDoor : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private InputManager _inputManager;
        private bool _isLooking = false;

        public GameObject winScreen;
        
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _inputManager = InputManager.Instance;

        }

        private void OnMouseEnter()
        {
            _isLooking = true;
        }

        private void OnMouseExit()
        {
            _isLooking = false;
        }

        private void Update()
        {
            if (Vector3.Distance(this.transform.position, _playerManager.player.transform.position) <= 2f)
            {
                _inputManager.OnInteractStarted += Exit;
            }
            else
            {
                _inputManager.OnInteractStarted -= Exit;
            }
        }

        private void Exit()
        {
            if (_playerManager.totalKeyItems == 5)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _inputManager.DisableMovementInputs();
                _inputManager.DisableLookInputs();
                winScreen.SetActive(true);
            }
        }
    }
}
