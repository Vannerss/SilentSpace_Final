using SilentSpace.Core;
using UnityEngine;
using SilentSpace.UI;

namespace SilentSpace.Player.interaction
{
    public class KeyItems : MonoBehaviour, IInteractable
    {
        private PlayerManager _playerManager;

        public string InteractionPrompt { get; }
        public string itemName;

        public Quest_Manager Quest_Manager;
        public UIManager objective;

        private void Start()
        {
          
            _playerManager = PlayerManager.Instance;
        }

        public void Interact()
        {
            if (itemName == "Crowbar")
            {
                Quest_Manager.crowbarPicked_Up = true;
                gameObject.SetActive(false);
            }

            if (Quest_Manager.crowbarPicked_Up)
            { 
                if (itemName == "EnergyCell")
                {
                    Quest_Manager.aquiredEnergyCell = true;
                    gameObject.SetActive(false);
                }
            }

            if (Quest_Manager.aquiredEnergyCell)
            {
                if (itemName == "ReactorConsole")
                {
                    Quest_Manager.generator_Turned_On = true;
                    gameObject.SetActive(false);
                    objective.CurrentOBJ = 2;
                }
            }

            if (Quest_Manager.Security_Reset)
            {
                if (itemName == "ReactorSurveillanceConsole")
                {
                    Quest_Manager.generator_Stabilized = true;
                    gameObject.SetActive(false);
                    objective.CurrentOBJ = 4;
                }

            }

            if (itemName == "CommsRoomConsole")
            {
                Quest_Manager.Security_Reset = true;
                gameObject.SetActive(false);
                objective.CurrentOBJ = 3;
            }


            if (Quest_Manager.generator_Stabilized)
            {
                if (itemName == "ID_Card")
                {
                    Quest_Manager.ID_Aquired = true;
                    gameObject.SetActive(false);
                    objective.CurrentOBJ = 5;
                }
            }


            if (itemName == "ShipSupplies 1")
            {
                Quest_Manager.Systems1_aquired = true;
                gameObject.SetActive(false);
            }



            if (itemName == "ShipSupplies 2")
            {
                Quest_Manager.Systems2_aquired = true;
                gameObject.SetActive(false);
            }



            if (itemName == "Food 1")
            {
                Quest_Manager.Food1_aquired = true;
                gameObject.SetActive(false);
            }



            if (itemName == "Food 2")
            {
                Quest_Manager.Food2_aquired = true;
                gameObject.SetActive(false);
            }



            if (itemName == "MedicalSupplies")
            {
                
                Quest_Manager.Medical_aquired = true;
                gameObject.SetActive(false);
            }



            if (itemName == "FinalConsole")
            {

                if (Quest_Manager.Food1_aquired || Quest_Manager.Food2_aquired || Quest_Manager.Systems1_aquired || Quest_Manager.Systems2_aquired
                    || Quest_Manager.Medical_aquired)

                {

                    Quest_Manager.Able_To_Exit = true;
                    gameObject.SetActive(false);
                    objective.CurrentOBJ = 6;
                }

            }

           
        }


    }
}
