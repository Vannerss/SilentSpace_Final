using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using UnityEngine;

namespace SilentSpace.Core
{
    //TODO: Setup LOADING logic with the GAME MANAGER.
    public class GameManager : MonoBehaviour, IDataPersistence
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

        public void LoadData(GameData data)
        {
            
        }

        public void SaveData(ref GameData data)
        {
            
        }
    }
}
