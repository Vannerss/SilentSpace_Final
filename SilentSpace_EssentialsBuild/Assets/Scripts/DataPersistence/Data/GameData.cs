namespace SilentSpace.DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        
        public PlayerData player;
        public AlienData alien;
        public JournalData journal;
        
        //Starting Values
        public GameData()
        {
            player = new PlayerData
            {
                hp = 100,
                oxygen = 100,
                totalKeyItems = 0,
                totalNoteItems = 0,
                xPos = 65,
                yPos = 0,
                zPos = -56,
                xRot = 0,
                yRot = -90,
                zRot = 0,
            };
            
            //TODO: Fill in correct default values for alien data.
            alien = new AlienData()
            {
                xPos = 0,
                yPos = 0,
                zPos = 0,
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
            };
        }

        [System.Serializable]
        public class PlayerData
        {
            public int hp;
            public int oxygen;
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
        }
    }
}
