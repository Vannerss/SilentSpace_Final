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
            noteItem1.interactable = data.noteItemButtonOne;
            noteItem2.interactable = data.noteItemButtonTwo;
            noteItem3.interactable = data.noteItemButtonThree;
            noteItem4.interactable = data.noteItemButtonFour;
            noteItem5.interactable = data.noteItemButtonFive;
        }

        public void SaveData(ref GameData data)
        {                                    
            data.noteItemButtonOne = noteItem1.interactable;
            data.noteItemButtonTwo = noteItem2.interactable;
            data.noteItemButtonThree = noteItem3.interactable;
            data.noteItemButtonFour = noteItem4.interactable;
            data.noteItemButtonFive = noteItem5.interactable;
        }
    }
}


