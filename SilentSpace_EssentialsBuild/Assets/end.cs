using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SilentSpace.Core;
using SilentSpace.Player.interaction;

namespace SilentSpace
{
    namespace SilentSpace.Player.interaction
{ 
    public class end : MonoBehaviour
        {
        public Quest_Manager quest_Manager;
        private PlayerManager _playerManager;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }


        public void Interact()
        {

            if (quest_Manager.Food1_aquired|| quest_Manager.Food2_aquired||quest_Manager.Systems1_aquired||quest_Manager.Systems2_aquired
                ||quest_Manager.Medical_aquired)

            {

                SceneManager.LoadScene("MainMenu");

            }


        }



        }
}
}
