using System.Collections;
using System.Collections.Generic;
using SilentSpace.Player.interaction;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class Chest : MonoBehaviour,IInteractable
    {
        [SerializeField] private string _prompt;
        public string InteractionPrompt => _prompt;

      
        public bool Interact(Interactor interactor)
        {
            Debug.Log("Opening chest!");
            return true;
        }
    }
}
