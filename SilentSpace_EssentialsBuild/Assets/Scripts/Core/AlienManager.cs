using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using UnityEngine;

namespace SilentSpace.Core
{
    public class AlienManager : MonoBehaviour, IDataPersistence
    {
        public static AlienManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void LoadData(GameData data)
        {
            var alienTransform = this.transform;
            
            alienTransform.position = data.alienPosition;
            alienTransform.eulerAngles = data.alienRotation;
        }

        public void SaveData(ref GameData data)
        {
            var alienTransform = this.transform;

            data.alienPosition = alienTransform.position;
            data.alienRotation = alienTransform.eulerAngles;
        }
    }
}