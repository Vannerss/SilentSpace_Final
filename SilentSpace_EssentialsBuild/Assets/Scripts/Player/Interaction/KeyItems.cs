using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class KeyItems : MonoBehaviour, IInteractable
    {
        private PlayerManager _playerManager;

        public string InteractionPrompt { get; }
        public string itemName;

        public Quest_Manager Quest_Manager;

        private void Start()
        {
          
            _playerManager = PlayerManager.Instance;
        }

        public void Interact()
        {
            if (itemName == "Crowbar")
            {
                Quest_Manager.crowbarPicked_Up = true;
            }

            if (itemName == "EnergyCell")
            {
                Quest_Manager.aquiredEnergyCell = true;
            }

            if (itemName == "ReactorConsole")
            {
                Quest_Manager.generator_Turned_On = true;
            }

            if (itemName == "ReactorSurveillanceConsole")
            {
                Quest_Manager.generator_Stabilized = true;
            }

            if (itemName == "CommsRoomConsole")
            {
                Quest_Manager.Security_Reset = true;
            }

            if (itemName == "ID_Card")
            {
                Quest_Manager.ID_Aquired = true;
            }

            if (itemName == "ShipSupplies 1")
            {
                Quest_Manager.Systems1_aquired = true;
            }

            if (itemName == "ShipSupplies 2")
            {
                Quest_Manager.Systems2_aquired = true;
            }

            if (itemName == "Food 1")
            {
                Quest_Manager.Food1_aquired = true;
            }

            if (itemName == "Food 2")
            {
                Quest_Manager.Food2_aquired = true;
            }

            if (itemName == "MedicalSupplies")
            {
                
                Quest_Manager.Medical_aquired = true;
            }



            gameObject.SetActive(false);
        }


    }
}
