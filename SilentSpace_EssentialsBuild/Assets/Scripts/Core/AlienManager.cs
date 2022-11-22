using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using UnityEngine;

namespace SilentSpace.Core
{
    public class AlienManager : MonoBehaviour, IDataPersistence
    {
        public static AlienManager Instance;
        public GameObject alien;
        
        private Transform _alienTransform;
        public ScriptableObject sc;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    
        private void Start()
        {
            _alienTransform = alien.transform;
        }

        private void SetAlienPosition(Vector3 position)
        {
            _alienTransform.position = position;
        }

        private void SetAlienRotation(Vector3 rotation)
        {
            _alienTransform.eulerAngles = rotation;
        }

        public void LoadData(GameData data)
        {
            var alienPos = Vector3.zero;
            var alienRot = Vector3.zero;

            alienPos.x = data.alien.xPos;
            alienPos.y = data.alien.yPos;
            alienPos.z = data.alien.zPos;
            alienRot.x = data.alien.xRot;
            alienRot.y = data.alien.yRot;
            alienRot.z = data.alien.zRot;

            SetAlienPosition(alienPos);
            SetAlienRotation(alienRot);
        }

        public void SaveData(ref GameData data)
        {
            var position = _alienTransform.position;
            var rotation = _alienTransform.eulerAngles;
            
            data.alien.xPos = position.x;
            data.alien.yPos = position.y;
            data.alien.zPos = position.z;
            data.alien.xRot = rotation.x;
            data.alien.yRot = rotation.y;
            data.alien.zRot = rotation.z;
        }
    }
}