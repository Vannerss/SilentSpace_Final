using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SilentSpace
{

    public class Quest_Manager : MonoBehaviour
    {
        public Light reactorTriggeredDoor1;
        public Light reactorTriggeredDoor2;
        public Light reactorTriggeredDoor3; 
        public Light Elevator1;
        public Light Elevator2;
        public Light finalDoor;
        public bool crowbarPicked_Up = false;
        public bool aquiredEnergyCell = false;
        public bool generator_Turned_On = false;
        public bool Security_Reset = false;
        public bool generator_Stabilized = false;
        public bool ID_Aquired = false;
        public bool Food1_aquired  = false;
        public bool Food2_aquired = false;
        public bool Systems1_aquired = false;
        public bool Systems2_aquired = false;
        public bool Medical_aquired = false;
        public bool Able_To_Exit = false;


        private void Update()
        {
            if(generator_Turned_On)
            {
                reactorTriggeredDoor1.color = Color.green;
                reactorTriggeredDoor2.color = Color.green;
                reactorTriggeredDoor3.color = Color.green;
                Elevator1.color = Color.green;
            }

            if(ID_Aquired)
            {
                Elevator2.color = Color.green;
            }

            if(Able_To_Exit)
            {
                finalDoor.color = Color.green;
            }
        }
    }
}
