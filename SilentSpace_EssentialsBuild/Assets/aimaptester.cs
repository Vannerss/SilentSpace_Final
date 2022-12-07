
using UnityEngine;
using UnityEngine.AI;

namespace SilentSpace
{
    public class aimaptester : MonoBehaviour
    {
        // Start is called before the first frame update
        private NavMeshAgent _agent;

        public Transform destination;
        
        void Start()
        {
            _agent = this.GetComponent<NavMeshAgent>();
            _agent.SetDestination(destination.position);
        }
    }
}
