using System;
using System.Collections;
using System.Collections.Generic;
using SilentSpace.Core;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class KeyItems : MonoBehaviour, IInteractable
    {
        private PlayerManager _playerManager;
        private AudioSource audio;

        public string InteractionPrompt { get; }

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            audio.Play();
        }

        public void Interact(Interactor interactor)
        {
                                                                                
        }
    }
}
