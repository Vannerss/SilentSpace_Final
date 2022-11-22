using UnityEngine;

namespace SilentSpace.Player.Camera
{
    public class MoveCamera : MonoBehaviour
    {
        public Transform cameraPosition;
        // Update is called once per frame
        void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}
