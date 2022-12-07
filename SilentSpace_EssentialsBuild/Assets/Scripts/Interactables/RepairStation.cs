using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Interactables
{
    public class RepairStation : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private InputManager _inputManager;
        private bool _isUsed;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _inputManager = InputManager.Instance;
        }

        private void OnMouseEnter()
        { 
            _inputManager.OnInteractStarted += Interacted; //Whenever E is pressed the method Interacted will be run.
        }

        private void OnMouseExit()
        { 
            _inputManager.OnInteractStarted -= Interacted; //Stop calling Interacted when E is pressed.
        }

        private void Interacted()
        {
            if (Vector3.Distance(_playerManager.player.transform.position, this.transform.position) < 2f && !_isUsed)
            {
                _playerManager.FullSuitFix();
                _isUsed = true;
            }
        }
    }
}
