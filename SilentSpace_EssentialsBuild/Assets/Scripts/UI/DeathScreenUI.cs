using SilentSpace.Core;
using SilentSpace.DataPersistence;
using UnityEngine.Device;

namespace SilentSpace.UI
{
    public class DeathScreenUI
    {
        private InputManager _inputManager;
        
        public void RestartFromSave()
        {
            _inputManager.EnableMovementInputs();
            _inputManager.EnableLookInputs();
            DataPersistenceManager.Instance.LoadGame();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}