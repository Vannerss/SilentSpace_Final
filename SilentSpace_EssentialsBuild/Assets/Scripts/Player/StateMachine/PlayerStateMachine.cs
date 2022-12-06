using SilentSpace.Audio;
using SilentSpace.Core;
using SilentSpace.Helpers;
using UnityEngine;

namespace SilentSpace.Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private string currentStateName;
        [SerializeField] private string currentSubStateName;
        [SerializeField] private Vector2 moveInput;
        
        private PlayerBaseState _currentState;
        private PlayerBaseState _currentSubState;
        private PlayerStateFactory _states;
        private AudioController _audioController;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private float _playerHeight = 1f;
        private float _speed;
        private float _startYScale;

        public Timer timer;
        public PlayerManager playerManager;
        public InputManager inputManager;
        public RaycastHit SlopeHit;
        public bool isRunning;
        public bool isCrouching;
        
        
        [Header("Movement")] 
        [Tooltip("Player speed while walking.")]
        public float walkSpeed = 5f;

        [Tooltip("Player speed while running.")]
        public float runSpeed = 10f;

        [Tooltip("Player speed while crouch walking.")]
        public float crouchSpeed = 2f;

        [Tooltip("Max angle the player can go up a slope.")]
        public float maxSlopeAngle = 35;
    
        [Header("Crouch Height")]
        public float crouchYScale = 0.7f;
        
        [Header("Facing Orientation")]
        public Transform orientation;
        
        [Header("Debug Logging")]
        public bool debug;

        #region SetGets
        public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }
        public PlayerBaseState CurrentSubState { get => _currentSubState; set => _currentSubState = value; }    
        public string CurrentStateName { get => currentStateName; set => currentStateName = value; }
        public string CurrentSubStateName { get => currentSubStateName;  set => currentSubStateName = value;  }
        public bool IsRunningPressed => isRunning;
        public bool IsCrouchingPressed => isCrouching;
        public Rigidbody PlayerRigidbody => _rb;
        public float Speed { get => _speed; set => _speed = value; }
        public Vector2 MoveInput => moveInput;
        public Vector3 MoveDirection { get => _moveDirection; set => _moveDirection = value; }
        public float PlayerHeight { get => _playerHeight; set => _playerHeight = value; }
        public float StartYScale => _startYScale;
        public AudioController Audio => _audioController;

        #endregion

        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            _currentState = _states.Idle();
            _currentState.EnterState();
        }

        void Start()
        {
            playerManager = PlayerManager.Instance;
            playerManager.OnPlayerOxygenRefilled += OxygenRecovered;
            playerManager.OnPlayerOxygenRanOut += OxygenRanOut;

            inputManager = InputManager.Instance;
            inputManager.OnCrouchHold += Crouch;
            inputManager.OnCrouchCanceled += Crouch;
            inputManager.OnRunHold += Run;
            inputManager.OnRunCanceled += Run;

            _audioController = AudioController.Instance;

            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _speed = walkSpeed;
            _startYScale = transform.localScale.y;
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdateState();
        }
        private void Update()
        {
            moveInput.x = inputManager.GetMovement().x;
            moveInput.y = inputManager.GetMovement().y;

            playerManager.CurrentSubState = currentSubStateName;

            _currentState.UpdateStates();

            if (playerManager.GetOxygen() <= 0)
            {
                inputManager.DisableRunInputs();
                isRunning = false;
            }
        }

        private void Crouch()
        {
            isCrouching = isCrouching != true;
        }

        private void Run()
        {
            isRunning = isRunning != true;
        }

        private void OxygenRanOut()
        {
            inputManager.DisableRunInputs();
            isRunning = false;
        }

        private void OxygenRecovered()
        {
            inputManager.EnableRunInputs();
        }
    
        /// <summary>
        /// Custom debug.log can be disable in inspector by setting debug to false.
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg)
        {
            if (!debug) return;
            print("[PlayerStateMachine]:" + msg);
        }
    }
}
