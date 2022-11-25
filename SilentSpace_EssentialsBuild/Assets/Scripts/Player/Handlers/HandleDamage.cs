using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.Handlers
{
    public class HandleDamage : MonoBehaviour
    {
        private PlayerManager _playerManager;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Hitbox")) return;
            var damage = _playerManager.GetHp() - 21; //replace the 21 with ENEMYMANAGAER.GETDMG();
            _playerManager.SetHp(damage);
            print("TookDmg");
        }
    }
}
