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
            
            var r = Random.Range(0, 20);
            var damage = _playerManager.GetHp() - 21; //replace the 21 with ENEMYMANAGAER.GETDMG();
            _playerManager.SetHp(damage);
            CheckForBrokenSuit(r);
            //_playerManager.SuitBroke();
            
            print("TookDmg");
        }
        
        private void CheckForBrokenSuit(int num)
        {
            switch (num)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 10:
                    _playerManager.SuitBroke();
                    break;
            }
        }
    }
}
