using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SilentSpace.UI;
namespace SilentSpace
{
    public class objectiveScreen : MonoBehaviour
    {

        public TextMeshProUGUI objectiveText;
        public int currentOBJ = 0; //this index starts counting from 0.
        public UIManager UIManager;
        private void OnEnable()
        {

            currentOBJ = UIManager.CurrentOBJ;

        }

        void Update()
        {

            currentOBJ = UIManager.CurrentOBJ;
            switch (currentOBJ)
            {
                case 0:
                    objectiveText.text = "The door to the generator is stuck, I need something to pry it open.";
                    break;
                
                case 1:
                    objectiveText.text = "the generator needs auxiliary power, there should be a power cell somwhere";
                    break;

                case 2:
                    objectiveText.text = "Security systems need to be restarted, since i got the generator on the backup power.";
                    break;

                case 3:
                    objectiveText.text = "Dammit, the generator is unstable, I need to get to the generator surveilance room to stabilize it";
                    break;

                case 4:
                    objectiveText.text = "Ok... with that solved, I need an ID card to get to the 2nd floor.";
                    break;

                case 5:
                    objectiveText.text = "Ok, almost there, I need to gather Food, ship systems, and medical supplies for the trip back." +
                        " if posible going to the containtment unit and recovering the research, before activating the console infront of the landing pad " +
                        "door.";
                    break;

                case 6:
                    objectiveText.text = "Leave the station.";
                    break;


            }

        }
    }
}
