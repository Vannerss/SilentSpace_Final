using System;
using JetBrains.Annotations;
using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using UnityEngine;

namespace SilentSpace.Core
{
    public class PlayerManager : MonoBehaviour, IDataPersistence
    {
        public static PlayerManager Instance;
        
        [SerializeField] private int health = 100;
        [SerializeField] private int oxygenLevel = 100;
        
        private string _currentState;
        private string _currentSubState;
        private Vector3 _playerPos;
        private Vector3 _playerRot;

        public int totalKeyItems;
        public int totalNoteItems;
        
        [Range(0.0f, 1.0f)] public float ySensitivity;
        [Range(0.0f, 1.0f)] public float xSensitivity;
        [CanBeNull] public GameObject player;
        public Transform spawnPoint;
        
        public event Action OnPlayerDeath; //TODO: Add death logic at some point.
            
        public int PickedUpItems { get; set; }
        public int Hp { get => health; set => health = value; }
        public Vector3 Position => player != null ? player.transform.position : Vector3.zero;

        public int Oxygen { get => oxygenLevel; set => oxygenLevel = value; }
        public string CurrentSubState { get => _currentState; set => _currentState = value; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (player != null)
            {
                _playerPos = player.transform.position;
                _playerRot = player.transform.rotation.eulerAngles;
            }
        }

        public int GetHp()
        {
            return health;
        }

        public void SetHp(int value)
        {
            health = value;
            if(health <= 0)
            {
                health = 0;
                OnPlayerDeath?.Invoke();
            }
            if(health > 100)
            {
                health = 100;
            }
        }
        
        #region Load & Save
        private void SetPlayerPosition(Vector3 position)
        {
            if (player != null) player.transform.position = position;
        }

        private void SetPlayerRotation(Vector3 rotation)
        {
            if (player != null) player.transform.eulerAngles = rotation;
        }
        
        public void LoadData(GameData data)
        {
            var posHolder = Vector3.zero;
            var rotHolder = Vector3.zero;
            
            this.health = data.player.hp;
            this.oxygenLevel = data.player.oxygen;
            this.totalKeyItems = data.player.totalKeyItems;
            this.totalKeyItems = data.player.totalNoteItems;
            posHolder.x = data.player.xPos;
            posHolder.y = data.player.yPos;
            posHolder.z = data.player.zPos;
            rotHolder.x = data.player.xRot;
            rotHolder.y = data.player.yRot;
            rotHolder.z = data.player.zRot;

            SetPlayerPosition(posHolder);
            SetPlayerRotation(rotHolder);
        }

        public void SaveData(ref GameData data)
        {
            data.player.hp = this.health;
            data.player.oxygen = this.oxygenLevel;
            data.player.totalKeyItems = this.totalKeyItems;
            data.player.totalNoteItems = this.totalNoteItems;
            data.player.xPos = _playerPos.x;
            data.player.yPos = _playerPos.y;
            data.player.zPos = _playerPos.z;
            data.player.xRot = _playerRot.x;
            data.player.yRot = _playerRot.y;
            data.player.zRot = _playerRot.z;
        }
        #endregion
    }
}
