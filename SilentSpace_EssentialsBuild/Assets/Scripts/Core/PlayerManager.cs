using System;
using System.Collections.Generic;
using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using UnityEngine;

namespace SilentSpace.Core
{
    public class PlayerManager : MonoBehaviour, IDataPersistence
    {
        public static PlayerManager Instance; 
        private string _currentState; 
        private string _currentSubState; 
        
        public float health = 100f; 
        public float oxygenLevel = 100f; 
        public int maxOxygenLevel = 100; 
        public bool isSuitDamaged = false; 
        public int totalKeyItems; 
        public int totalNoteItems; 
        [Range(0.0f, 1.0f)] public float ySensitivity; 
        [Range(0.0f, 1.0f)] public float xSensitivity; 
        public GameObject player; 
        public Transform spawnPoint; 
        public HashSet<string> inventory;

        public event Action OnPlayerDeath; 
        public event Action OnPlayerOxygenRanOut; 
        public event Action OnPlayerOxygenRefilled; 
        
        
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

        private void FixedUpdate()
        {
            SetOxygen(oxygenLevel + 0.1f);
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
            if (health <= 0)
            {
                health = 0;
                OnPlayerDeath?.Invoke();
            }
            
            if (health > 100)
            {
                health = 100;
            }
        }

        public void SetOxygen(float value)
        {
            if (value > maxOxygenLevel)
            {
                oxygenLevel = maxOxygenLevel;
                OnPlayerOxygenRefilled?.Invoke();
            }
            else if (value <= 0)
            {
                oxygenLevel = 0;
                OnPlayerOxygenRanOut?.Invoke();
            }
            else
            {
                oxygenLevel = value;
            }
        }

        public void AddToInventory(string itemName)
        {
           
            
            
        }
        
        public bool InventoryHas(string itemName)
        {
            return true;
        }
        
        public void SuitBroke()
        {
            isSuitDamaged = true;
            maxOxygenLevel -= 25;
            if (maxOxygenLevel <= 25)
            {
                maxOxygenLevel = 25;
            }

            if (oxygenLevel > maxOxygenLevel)
            {
                SetOxygen(maxOxygenLevel);
            }
            Debug.Log(maxOxygenLevel);
        }

        public void PartialSuitFix()
        {
            if (maxOxygenLevel != 100)
            {
                maxOxygenLevel += 25;
            }
            else
            {
                isSuitDamaged = false;
            }
        }

        public void FullSuitFix()
        {
            Debug.Log("Full Fix");
            maxOxygenLevel = 100;
            isSuitDamaged = false;
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
