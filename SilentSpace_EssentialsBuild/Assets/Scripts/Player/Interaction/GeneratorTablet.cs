using System;
using System.Collections;
using System.Collections.Generic;
using SilentSpace.Core;
using SilentSpace.Player.interaction;
using UnityEngine;

namespace SilentSpace
{
    public class GeneratorTablet : MonoBehaviour, IInteractable
    {
        public string InteractionPrompt { get; }

        private PlayerManager _playerManager;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }


        public void Interact(Interactor interactor)
        {
            _playerManager.SetHp(_playerManager.GetHp() - 20); 
            
            _playerManager.AddToInventory("GeneratorOn");
        }
    }
}
