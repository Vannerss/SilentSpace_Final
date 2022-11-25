namespace SilentSpace.DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        //TODO: This doesn't really work probably do to attempting to divide data in different classes. probably struct is better or just straight up call them.
        
        
        public PlayerData player;
        public AlienData alien;
        public JournalData journal;
        public SceneData scene;
        
        //Starting Values
        public GameData()
        {
            player = new PlayerData
            {
                hp = 100f,
                oxygen = 100f,
                totalKeyItems = 0,
                totalNoteItems = 0,
                xPos = -31.19f,
                yPos = 0,
                zPos = 8.11f,
                xRot = 0,
                yRot = 0,
                zRot = 0,
            };
            
            alien = new AlienData()
            {
                xPos = -31.48f,
                yPos = 0,
                zPos = 49.2f,
                xRot = 0,
                yRot = 0,
                zRot = 0,
            };

            journal = new JournalData()
            {
                keyItemOne = true,
                keyItemTwo = true,
                keyItemThree = true,
                keyItemFour = true,
                keyItemFive = true,
                
                noteItemOne = false,
                noteItemTwo = false,
                noteItemThree = false,
                noteItemFour = false,
                noteItemFive = false,
            };

            scene = new SceneData()
            {
                keyItemOne = true,
                keyItemTwo = true,
                keyItemThree = true,
                keyItemFour = true,
                keyItemFive = true,

                noteItemOne = false,
                noteItemTwo = false,
                noteItemThree = false,
                noteItemFour = false,
                noteItemFive = false,
            };
        }

        [System.Serializable]
        public class PlayerData
        {
            public float hp;
            public float oxygen;
            public int totalKeyItems;
            public int totalNoteItems;
            public float xPos;
            public float yPos;
            public float zPos;
            public float xRot;
            public float yRot;
            public float zRot;
        }

        [System.Serializable]
        public class AlienData
        {
            public float xPos;
            public float yPos;
            public float zPos;
            public float xRot;
            public float yRot;
            public float zRot;
        }

        [System.Serializable]
        public class JournalData
        {
            public bool keyItemOne;
            public bool keyItemTwo;
            public bool keyItemThree;
            public bool keyItemFour;
            public bool keyItemFive;
            
            public bool noteItemOne;
            public bool noteItemTwo;
            public bool noteItemThree;
            public bool noteItemFour;
            public bool noteItemFive;
        }
        
        [System.Serializable]
        public class SceneData
        {
            public bool keyItemOne;
            public bool keyItemTwo;
            public bool keyItemThree;
            public bool keyItemFour;
            public bool keyItemFive;
            
            public bool noteItemOne;
            public bool noteItemTwo;
            public bool noteItemThree;
            public bool noteItemFour;
            public bool noteItemFive;
        }
    }
}
