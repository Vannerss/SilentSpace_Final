using System;
using System.Collections;
using System.Collections.Generic;
using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class DuctTape : MonoBehaviour, IInteractable
    {

        private PlayerManager _playerManager;
        
        public string InteractionPrompt { get; }


        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        public void Interact()
        {
            _playerManager.PartialSuitFix();
            gameObject.SetActive(false);
        }
    }
}
