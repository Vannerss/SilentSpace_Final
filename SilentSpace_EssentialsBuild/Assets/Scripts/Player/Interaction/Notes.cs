using System.Collections.Generic;
using SilentSpace.Player.interaction;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.Player.Interaction
{
    public class Notes : MonoBehaviour, IInteractable
    {
        public string InteractionPrompt { get; }

        public Button noteButton;
        
        public void Interact()
        {
            noteButton.interactable = true;
            gameObject.SetActive(false);
        }
    }
}