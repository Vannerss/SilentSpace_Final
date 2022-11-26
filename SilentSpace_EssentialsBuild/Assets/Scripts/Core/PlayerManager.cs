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

        public int totalKeyItems;
        public int totalNoteItems;
        
        [Range(0.0f, 1.0f)] public float ySensitivity;
        [Range(0.0f, 1.0f)] public float xSensitivity;
        public GameObject player;
        public Transform spawnPoint;
        
        public event Action OnPlayerDeath;
        
        
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

        private void Update()
        {
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
        public void LoadData(GameData data)
        {
            this.health = data.playerHp;
            this.oxygenLevel = data.playerOxygen;
            this.totalKeyItems = data.playerKeyItems;
            this.totalNoteItems = data.playerNoteItems;
            this.player.transform.position = data.playerPosition;
            this.player.transform.eulerAngles = data.playerRotation;
        }
        
        public void SaveData(ref GameData data)
        {
            data.playerHp = this.health;
            data.playerOxygen = this.oxygenLevel;
            data.playerKeyItems = this.totalKeyItems;
            data.playerNoteItems = this.totalNoteItems;
            data.playerPosition = this.player.transform.position;
            data.playerRotation = this.player.transform.eulerAngles;
        }
        #endregion
    }
}
