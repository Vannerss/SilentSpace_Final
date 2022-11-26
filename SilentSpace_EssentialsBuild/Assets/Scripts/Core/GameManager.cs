using System;
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

        public GameObject keyObject1;
        public GameObject keyObject2;
        public GameObject keyObject3;
        public GameObject keyObject4;
        public GameObject keyObject5;
        public GameObject noteObject1;
        public GameObject noteObject2;
        public GameObject noteObject3;
        public GameObject noteObject4;
        public GameObject noteObject5;

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
            keyObject1.SetActive(data.keyItemObjectOne);
            keyObject2.SetActive(data.keyItemObjectTwo);
            keyObject3.SetActive(data.keyItemObjectThree);
            keyObject4.SetActive(data.keyItemObjectFour);
            keyObject5.SetActive(data.keyItemObjectFive);
            
            noteObject1.SetActive(data.noteItemObjectOne);
            noteObject2.SetActive(data.noteItemObjectTwo);
            noteObject3.SetActive(data.noteItemObjectThree);
            noteObject4.SetActive(data.noteItemObjectFour);
            noteObject5.SetActive(data.noteItemObjectFive);
        }
        
        public void SaveData(ref GameData data)
        {
            data.keyItemObjectOne = keyObject1.activeSelf;
            data.keyItemObjectTwo = keyObject2.activeSelf;
            data.keyItemObjectThree = keyObject3.activeSelf;
            data.keyItemObjectFour = keyObject4.activeSelf;
            data.keyItemObjectFive = keyObject5.activeSelf;
        
            data.noteItemObjectOne = noteObject1.activeSelf;
            data.noteItemObjectTwo = noteObject2.activeSelf;
            data.noteItemObjectThree = noteObject3.activeSelf;
            data.noteItemObjectFour = noteObject4.activeSelf;
            data.noteItemObjectFive = noteObject5.activeSelf;
        }
    }
}
