using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace
{
    public class objectivePrompt : MonoBehaviour
    {

        public Image Image;
        public GameObject Position1;
        public GameObject Position2;

        public int currentPosition = 0;
        // Start is called before the first frame update
        void Update()
        {
            if(currentPosition == 2)
            {
                Image.transform.position = Vector3.Lerp(Image.transform.position, Position2.transform.position, 2.5f * Time.deltaTime);
         
            }

            if(Input.GetKeyDown(KeyCode.J))
            {
                currentPosition = 1;

            }

            if (currentPosition == 1)
            {
               
                Image.transform.position = Vector3.Lerp(Image.transform.position, Position1.transform.position, 2.5f * Time.deltaTime);

            }
           

        }
    }
}
