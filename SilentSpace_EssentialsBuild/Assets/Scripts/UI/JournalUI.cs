using TMPro;
using UnityEngine;

namespace SilentSpace.UI
{
    public class JournalUI : MonoBehaviour
    {
        public GameObject objectivesUI;
        public GameObject notesUI;
        public GameObject noteUI;
        public TMP_Text noteBlock;
        public string[] noteTxt;

        public void OpenObjectives()
        {
            objectivesUI.SetActive(true);
            notesUI.SetActive(false);
            noteUI.SetActive(false);
        }   
    
        public void OpenNotes()
        {
            objectivesUI.SetActive(false);
            notesUI.SetActive(true);
            noteUI.SetActive(false);
        }

        public void ChooseNote(int note)
        {
            objectivesUI.SetActive(false);
            notesUI.SetActive(false);
            noteUI.SetActive(true);
            noteBlock.text = noteTxt[note];
        }
        
    }
}
