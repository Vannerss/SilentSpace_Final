using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public interface IInteractable
    {
        public string InteractionPrompt {  get; }

        public bool Interact(Interactor interactor);
    }
} 
              