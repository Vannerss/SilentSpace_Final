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

        public void EnableHitbox()
        {
            hitbox.SetActive(true);
            print("It Enabled");
        }

        public void DisableHitbox()
        {
            hitbox.SetActive(false);
            print("It Disabled");
        }
    }
}
