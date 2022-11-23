using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.UI
{
    public class NotesUI : MonoBehaviour, IDataPersistence
    {
        private TMP_Text _noteTextBlock;

        public Notes[] notesArray = new Notes[5];
        
        public Button noteItem1;
        public Button noteItem2;
        public Button noteItem3;
        public Button noteItem4;
        public Button noteItem5;
        public GameObject noteObject1;
        public GameObject noteObject2;
        public GameObject noteObject3;
        public GameObject noteObject4;
        public GameObject noteObject5;
        

        private void Start()
        {
            _noteTextBlock = GameObject.Find("NoteTxt").GetComponent<TMP_Text>();
        }
    
        [System.Serializable]
        public class Notes
        {
            [TextArea(15, 35)]
            public string text;
        }

        public void DisplayNote(int index)
        {
            _noteTextBlock.text = notesArray[index].text;
        }

        public void LoadData(GameData data)
        {
            noteItem1.interactable = data.journal.noteItemOne;
            noteItem2.interactable = data.journal.noteItemTwo;
            noteItem3.interactable = data.journal.noteItemThree;
            noteItem4.interactable = data.journal.noteItemFour;
            noteItem5.interactable = data.journal.noteItemFive;
            
            noteObject1.SetActive(data.scene.noteItemOne);
            noteObject2.SetActive(data.scene.noteItemTwo);
            noteObject3.SetActive(data.scene.noteItemThree);
            noteObject4.SetActive(data.scene.noteItemFour);
            noteObject5.SetActive(data.scene.noteItemFive);
        }

        public void SaveData(ref GameData data)
        {                                    
            data.journal.noteItemOne   = noteItem1.interactable;
            data.journal.noteItemTwo   = noteItem2.interactable;
            data.journal.noteItemThree = noteItem3.interactable;
            data.journal.noteItemFour  = noteItem4.interactable;
            data.journal.noteItemFive  = noteItem5.interactable;

            data.scene.noteItemOne = noteObject1.activeSelf;
            data.scene.noteItemTwo = noteObject2.activeSelf;
            data.scene.noteItemThree = noteObject3.activeSelf;
            data.scene.noteItemFour = noteObject4.activeSelf;
            data.scene.noteItemFive = noteObject5.activeSelf;
        }
    }
}


