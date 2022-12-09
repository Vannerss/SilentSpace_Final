using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.CharacterCamera
{
    public class CamController : MonoBehaviour
    {
        private InputManager _inputManager;
        private PlayerManager _playerManager;
        private float _xRot;
        private float _yRot;

        public Transform orientation;

        private void Start()
        {
            _inputManager = InputManager.Instance;
            _playerManager = PlayerManager.Instance;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            var mouseX = _inputManager.GetMouseMovement().x * _playerManager.xSensitivity;
            var mouseY = _inputManager.GetMouseMovement().y * _playerManager.ySensitivity;
            
            _yRot += mouseX;
            _xRot -= mouseY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);

            transform.rotation = Quaternion.Euler(_xRot, _yRot, 0);
            orientation.rotation = Quaternion.Euler(0, _yRot, 0);
        }
    }
}
