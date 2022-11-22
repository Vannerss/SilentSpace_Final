using UnityEngine;

namespace SilentSpace.UI
{
    public class JournalUI : MonoBehaviour
    {
        public GameObject objectivesUI;
        public GameObject notesUI;

        public void OpenObjectives()
        {
            objectivesUI.SetActive(true);
            notesUI.SetActive(false);
        }   
    
        public void OpenNotes()
        {
            objectivesUI.SetActive(false);
            notesUI.SetActive(true);
        }
    }
}
