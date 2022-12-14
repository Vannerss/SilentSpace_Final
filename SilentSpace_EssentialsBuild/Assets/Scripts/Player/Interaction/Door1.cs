using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class Door1 : MonoBehaviour,IInteractable
    {
        [SerializeField] private string _prompt;
        public string InteractionPrompt => _prompt;

        
        public void Interact()
        {
     
        }
    }
}
