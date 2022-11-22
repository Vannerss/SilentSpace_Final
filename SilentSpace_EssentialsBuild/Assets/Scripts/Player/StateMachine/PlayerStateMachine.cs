using SilentSpace.Audio;
using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private Vector2 moveInput;
        [SerializeField] private bool isRunning;
        [SerializeField] private bool isCrouching;
    
        private PlayerManager _playerManager;
        private InputManager _inputManager;
        private AudioController _audioController;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private float _speed;
        private float _startYScale;
    
        //[FormerlySerializedAs("_walkSpeed")]
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

        //Slope Variables
        private float _playerHeight = 1f;
        public RaycastHit SlopeHit;

        //state variable
        private PlayerBaseState _currentState;
        private PlayerBaseState _currentSubState;
        private PlayerStateFactory _states;

        //string statename;
        [SerializeField] private string currentStateName;
        [SerializeField] private string currentSubStateName;

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
        public PlayerStateFactory States => _states;
        #endregion

        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            _currentState = _states.Idle();
            _currentState.EnterState();
        }

        void Start()
        {
            _playerManager = PlayerManager.Instance;

            _inputManager = InputManager.Instance;
            _inputManager.OnCrouchHold += Crouch;
            _inputManager.OnCrouchCanceled += Uncrouch;
            _inputManager.OnRunHold += Run;
            _inputManager.OnRunCanceled += StopRun;

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
            moveInput.x = _inputManager.GetMovement().x;
            moveInput.y = _inputManager.GetMovement().y;

            _playerManager.CurrentSubState = currentSubStateName;

            _currentState.UpdateStates();
        }

        private void Crouch()
        {
            isCrouching = true;
        }

        private void Uncrouch()
        {
            isCrouching = false;
        }

        private void Run()
        {
            isRunning = true;
        }

        private void StopRun()
        {
            isRunning = false;
        }

        public void Log(string msg)
        {
            if (!debug) return; //guard clause'
            print("[PlayerStateMachine]:" + msg);
        }
    }
}
