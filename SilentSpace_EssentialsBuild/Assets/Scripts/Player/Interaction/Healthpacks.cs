using System;
using System.Collections;
using System.Collections.Generic;
using SilentSpace.Core;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.Player.interaction
{
    public class Healthpacks : MonoBehaviour,IInteractable
    {
        [SerializeField] private string _prompt;
        private PlayerManager _playerManager;
        public float healthBonus;
        public string InteractionPrompt => _prompt;


        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        public void Interact()
        {
            var healingAmount = _playerManager.GetHp() + healthBonus;
            _playerManager.SetHp(healingAmount);
            gameObject.SetActive(false);
        }




    }
}
