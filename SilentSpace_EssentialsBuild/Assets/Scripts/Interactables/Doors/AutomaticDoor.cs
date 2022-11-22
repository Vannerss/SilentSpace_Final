using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Interactables.Doors
{
    public class AutomaticDoor : MonoBehaviour
    {
    
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private GameObject rightDoor;
    
        private PlayerManager _playerManager;
    
        private Vector3 _startingPositionLeft;
        private Vector3 _startingPositionRight;
        private Vector3 _goalPositionLeft;
        private Vector3 _goalPositionRight;

        [Header("Door Behaviour")]
        [Tooltip("Defines how far away from the player or alien does the door opens.")]
        public float detectionRange = 3f;
        [Tooltip("Defines how much distance the doors move while opening.")]
        public float maxOpenDistance = 1f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, detectionRange);
        }

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            leftDoor = this.transform.GetChild(0).gameObject;
            rightDoor = this.transform.GetChild(1).gameObject;

            var leftDoorPos = leftDoor.transform.position;
            var rightDoorPos = rightDoor.transform.position;
            _startingPositionLeft = leftDoorPos;
            _startingPositionRight = rightDoorPos;
            _goalPositionLeft = new Vector3(leftDoorPos.x, leftDoorPos.y, leftDoorPos.z + maxOpenDistance);
            _goalPositionRight = new Vector3(rightDoorPos.x, rightDoorPos.y, rightDoorPos.z - maxOpenDistance);
        }

        private void Update()
        {
            if (Vector3.Distance(_playerManager.Position, this.transform.position ) <= detectionRange)
            {
                leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, _goalPositionLeft, 3 * Time.deltaTime);
                rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, _goalPositionRight, 3 * Time.deltaTime);
            }
            else
            {
                leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, _startingPositionLeft, 3 * Time.deltaTime);
                rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, _startingPositionRight, 3 * Time.deltaTime);
            }
        }
    }
}
