using System;
using System.Collections.Generic;
using System.Linq;
using SilentSpace.DataPersistence.Data;
using UnityEngine;

namespace SilentSpace.DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")]
        [SerializeField] private string fileName;

        public GameData gameData;
        private List<IDataPersistence> _dataPersistenceObjects;
        private FileDataHandler _dataHandler;
        public static DataPersistenceManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            }
            Instance = this;
            //LoadGame();
        }

        private void Start()
        {
            this._dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this._dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        public void NewGame()
        {
            this.gameData = new GameData();
            SaveGame();
        }

        public void LoadGame()
        {
            this.gameData = _dataHandler.Load();

            if (this.gameData == null)
            {
                Debug.Log("No data was found. Initializing to default values.");
                NewGame();
            }

            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }

        public void SaveGame()
        {
            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }

            _dataHandler.Save(gameData);
        }

        public void QuitAndSave()
        {
            SaveGame();
            Application.Quit();
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
