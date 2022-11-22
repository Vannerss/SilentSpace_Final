using TMPro;
using UnityEngine;

namespace SilentSpace.UI
{
    public class NotesUI : MonoBehaviour
    {
        private TMP_Text _noteTextBlock;

        public Notes[] notesArray = new Notes[5];

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
    }
}


