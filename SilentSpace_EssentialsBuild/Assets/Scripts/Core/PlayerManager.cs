using System;
using System.Collections;
using JetBrains.Annotations;
using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using UnityEngine;

namespace SilentSpace.Core
{
    public class PlayerManager : MonoBehaviour, IDataPersistence
    {
        public static PlayerManager Instance;
        
        public float health = 100;
        public float oxygenLevel = 100;
        
        private string _currentState;
        private string _currentSubState;
        private Vector3 _playerPos;
        private Vector3 _playerRot;

        public int totalKeyItems;
        public int totalNoteItems;
        
        [Range(0.0f, 1.0f)] public float ySensitivity;
        [Range(0.0f, 1.0f)] public float xSensitivity;
        public GameObject player;
        public Transform spawnPoint;
        
        public event Action OnPlayerDeath;
            
        public int PickedUpItems { get; set; }
        public float Hp { get => health; set => health = value; }
        public Vector3 Position => player != null ? player.transform.position : Vector3.zero;
        public float Oxygen { get => oxygenLevel; set => oxygenLevel = value; }
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

        private void Update()
        {
            Debug.Log(oxygenLevel);
            if (oxygenLevel <= 0f)
            {
                SetHp(health - 0.10f);
            }
        }

        public float GetHp()
        {
            return health;
        }

        public float GetOxygen()
        {
            return oxygenLevel;
        }

        public void SetHp(float value)
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
            var posHolder = player.transform.position;
            var rotHolder = player.transform.eulerAngles;
            
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
        }

        public void SaveData(ref GameData data)
        {

            data.player.hp = this.health;
            data.player.oxygen = this.oxygenLevel;
            data.player.totalKeyItems = this.totalKeyItems;
            data.player.totalNoteItems = this.totalNoteItems;
            data.player.xPos = player.transform.position.x;
            data.player.yPos = player.transform.position.y;
            data.player.zPos = player.transform.position.z;
            data.player.xRot = player.transform.eulerAngles.x;
            data.player.yRot = player.transform.eulerAngles.y;
            data.player.zRot = player.transform.eulerAngles.z;
        }
        #endregion
    }
}
