using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class Interactor : MonoBehaviour
    {
        private IInteractable _interactable;
        private InputManager _inputManager;
        public Transform camTransform;
        
        public LayerMask interactableMask;
        public float interactionMaxDistance;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(camTransform.position, camTransform.forward);
        }
        private void Start()
        {
            _inputManager = InputManager.Instance;
            _inputManager.OnInteractStarted += Ray;
        }

        private void Ray()
        {
            var ray = new Ray(camTransform.position, camTransform.forward);
            
            if (Physics.Raycast(ray, out var hit, interactionMaxDistance, interactableMask))
            {
                _interactable = hit.collider.GetComponent<IInteractable>();
                _interactable.Interact();
            }
        }
    }
}