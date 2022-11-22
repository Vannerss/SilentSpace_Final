using UnityEngine;

namespace SilentSpace.Helpers
{
    public class Gizmo : MonoBehaviour
    {
        public Color color;
        public float gizmoRange;
        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(this.transform.position, gizmoRange);
        }
    }
}

