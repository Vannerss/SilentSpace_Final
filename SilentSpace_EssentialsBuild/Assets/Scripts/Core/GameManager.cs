using UnityEngine;

namespace SilentSpace.Core
{
    //TODO: Setup LOADING logic with the GAME MANAGER.
    public class GameManager : MonoBehaviour
    {
        //Singleton
        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); //seppuku.
            }
        }

    }
}
