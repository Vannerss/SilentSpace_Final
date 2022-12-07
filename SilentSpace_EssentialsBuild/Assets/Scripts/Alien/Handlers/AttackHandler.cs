using UnityEngine;

namespace SilentSpace.Alien.Handlers
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private GameObject hitbox;

        private void Start()
        {
            hitbox.SetActive(false);
        }
        /// <summary>
        /// Triggered by animation.
        /// </summary>
        public void EnableHitbox()
        {
            hitbox.SetActive(true);
        }

        /// <summary>
        /// Triggered by animation.
        /// </summary>
        public void DisableHitbox()
        {
            hitbox.SetActive(false);
        }
    }
}
