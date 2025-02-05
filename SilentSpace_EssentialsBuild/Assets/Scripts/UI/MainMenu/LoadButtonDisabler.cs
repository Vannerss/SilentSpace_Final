using SilentSpace.DataPersistence;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.UI.MainMenu
{
    public class LoadButtonDisabler : MonoBehaviour
    {
        private DataPersistenceManager _dataPersistenceManager;
        
        public Button loadGameButton;

        private void Start()
        {
            _dataPersistenceManager = DataPersistenceManager.Instance;
            if (_dataPersistenceManager.gameData != null) MakeLoadInteractable();
        }

        private void MakeLoadInteractable()
        {
            loadGameButton.interactable = true;
        }
    }
}
