 using SilentSpace.Core;
using SilentSpace.Helpers;
using UnityEngine;
using UnityEngine.AI;
using SilentSpace.Audio;

namespace SilentSpace.Alien.StateMachine
{
    public class AlienStateMachine : MonoBehaviour
    {
        [SerializeField] private string previousStateName;
        [SerializeField] private string currentStateName;

        private AlienBaseState _currentState;
        private AlienBaseState _currentSubState;
        private AlienStateFactory _states;
        private Transform _transform;

        public AreaTransforms[] areas;
        public PlayerManager playerManager;
        public AudioController audioController;
        public NavMeshAgent agent;
        public Animator animator;
        public bool debug;
        public bool gizmos;
        public float range;
        public float detectRun;
        public float detectWalk;
        public float detectCrouch;
        public Timer Timer;
        [HideInInspector] public bool timerIsRunning;
        
        public AlienBaseState CurrentState { get => _currentState; set => _currentState = value; }
        public string PreviousStateName { get => previousStateName;  set => previousStateName = value;  }
        public string CurrentStateName { get => currentStateName; set => currentStateName = value;  }
        public Vector3 Position => _transform.position;
        
        [System.Serializable]
        public class AreaTransforms
        {
            public Transform[] transforms;
        }
        
        private void OnDrawGizmos()
        {
            if (!gizmos) return;
            var pos = this.transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pos, detectCrouch);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(pos, detectWalk);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pos, detectRun);
        }
        
        private void Awake()
        {
            _states = new AlienStateFactory(this);
        }

        private void Start()
        {
            _transform = this.transform;
            agent = this.gameObject.GetComponent<NavMeshAgent>();
            playerManager = PlayerManager.Instance;
            audioController = AudioController.Instance;
            animator = this.gameObject.GetComponent<Animator>();
            _currentState = _states.Roam();
            _currentState.EnterState();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdateState();
        }
        
        private void Update()
        {
            _currentState.UpdateStates();
        }

        public void Log(string msg)
        {
            if (debug)
            {
                Debug.Log(msg);
            }
        }
    }
}
