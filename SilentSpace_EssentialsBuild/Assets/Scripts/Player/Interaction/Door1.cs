using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class Door1 : MonoBehaviour,IInteractable
    {
        [SerializeField] private string _prompt;
        public string InteractionPrompt => _prompt;

        
        public bool Interact(Interactor interactor)
        {
            var inventory = interactor.GetComponent<Inventory>();

            if (inventory == null) return false; 
            if (inventory.HasKey)
            {
                Debug.Log("Opening door!");   
                return true; 
            }

            Debug.Log("No key found");
            return false;

        }
    }
}
