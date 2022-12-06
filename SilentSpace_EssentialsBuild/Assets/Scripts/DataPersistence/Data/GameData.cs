using UnityEngine;

namespace SilentSpace.DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        #region DATA
        
        public float playerHp;
        public float playerOxygen;
        public int playerKeyItems;
        public int playerNoteItems;
        public Vector3 playerPosition;
        public Vector3 playerRotation;

        public Vector3 alienPosition;
        public Vector3 alienRotation;

        public bool keyItemButtonOne;
        public bool keyItemButtonTwo;
        public bool keyItemButtonThree;
        public bool keyItemButtonFour;
        public bool keyItemButtonFive;
        
        public bool noteItemButtonOne;
        public bool noteItemButtonTwo;
        public bool noteItemButtonThree;
        public bool noteItemButtonFour;
        public bool noteItemButtonFive;
        
        public bool keyItemObjectOne;
        public bool keyItemObjectTwo;
        public bool keyItemObjectThree;
        public bool keyItemObjectFour;
        public bool keyItemObjectFive;
        
        public bool noteItemObjectOne;
        public bool noteItemObjectTwo;
        public bool noteItemObjectThree;
        public bool noteItemObjectFour;
        public bool noteItemObjectFive;
        
        #endregion
        
        public GameData()
        {
            //Player Data.
            this.playerHp = 100f;
            this.playerOxygen = 100f;
            this.playerKeyItems = 0;
            this.playerNoteItems = 0;
            this.playerPosition = Vector3.zero; //TODO: Change for starting position.
            this.playerRotation = Vector3.zero; //TODO: Change for starting rotation.

            //Alien Data.
            this.alienPosition = Vector3.zero; //TODO: Change for starting position.
            this.alienRotation = Vector3.zero; //TODO: Change for starting rotation.

            //UI Buttons Interactable State.
            this.keyItemButtonOne = true;
            this.keyItemButtonTwo = true;
            this.keyItemButtonThree = true;
            this.keyItemButtonFour = true;
            this.keyItemButtonFive = true;
            
            this.noteItemButtonOne = false;
            this.noteItemButtonTwo = false;
            this.noteItemButtonThree = false;
            this.noteItemButtonFour = false;
            this.noteItemButtonFive = false;
            
            //Objects state on scene.
            this.keyItemObjectOne = true;
            this.keyItemObjectTwo = true;
            this.keyItemObjectThree = true;
            this.keyItemObjectFour = true;
            this.keyItemObjectFive = true;

            this.noteItemObjectOne = true;
            this.noteItemObjectTwo = true;
            this.noteItemObjectThree = true;
            this.noteItemObjectFour = true;
            this.noteItemObjectFive = true;
        }
    }
}
