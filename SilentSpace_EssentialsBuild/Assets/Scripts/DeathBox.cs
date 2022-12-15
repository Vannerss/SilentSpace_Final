using System;
using System.Collections;
using System.Collections.Generic;
using SilentSpace.UI;
using UnityEngine;

namespace SilentSpace
{
    public class DeathBox : MonoBehaviour
    {
        public UIManager uiManager;

        private void OnTriggerEnter(Collider other)
        {
            uiManager.OpenDeathMenu();
        }
    }
}
