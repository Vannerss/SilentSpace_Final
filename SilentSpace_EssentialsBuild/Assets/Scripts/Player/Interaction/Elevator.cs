using UnityEngine;
using SilentSpace.Core;

namespace SilentSpace.Player.interaction
{
    public class Elevator : MonoBehaviour, IInteractable
    {
        public string InteractionPrompt { get; }
        
        public GameObject elevator;
        public Vector3 goalPosition;
        public float addToPosition;
        public PlayerManager playerManager;

        private Vector3 _startingPosition;
        private bool _goUp;
        private bool _elevatorIsUp; //-76.55862 -26.97554 -36.00361
        //0.1444578
        
        private void Start()
        {
            playerManager = PlayerManager.Instance;
            var elevatorPos = elevator.transform.position;
            _startingPosition = elevatorPos;
            goalPosition = new Vector3(elevatorPos.x, elevatorPos.y + addToPosition, elevatorPos.z);
        }

        public void Interact()
        {
            if(!_elevatorIsUp) _goUp = true;
            //goalPosition = Vector3.zero;
            Debug.Log("hi");
            playerManager.health = 10f;
        }

        private void Update()
        {
            if(_goUp)
            {
                //elevator.transform.position = Vector3.Lerp(elevator.transform.position, goalPosition, 0.2f * Time.deltaTime);
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, goalPosition, 2 * Time.deltaTime);
                
                if (elevator.transform.position == goalPosition)
                {
                    _goUp = false;
                    _elevatorIsUp = true;
                }
            }
        }
    }
}