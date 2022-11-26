using JetBrains.Annotations;
using SilentSpace.DataPersistence;
using SilentSpace.DataPersistence.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SilentSpace.UI
{
    public class ObjectivesUI : MonoBehaviour, IDataPersistence
    {
        private TMP_Text _hintTextBlock;

        public Hints[] hintsArray = new Hints[5];

        public Button keyItem1;
        public Button keyItem2;
        public Button keyItem3;
        public Button keyItem4;
        public Button keyItem5;
        
        private void Start()
        {
            _hintTextBlock = GameObject.Find("HintTxt").GetComponent<TMP_Text>();
        }
    
        [System.Serializable]
        public class Hints
        {
            [TextArea(15, 35)]
            public string text;
        }

        public void DisplayHint(int index)
        {
            _hintTextBlock.text = hintsArray[index].text;
        }

        public void LoadData(GameData data)
        {
            keyItem1.interactable = data.keyItemButtonOne;
            keyItem2.interactable = data.keyItemButtonTwo;
            keyItem3.interactable = data.keyItemButtonThree;
            keyItem4.interactable = data.keyItemButtonFour;
            keyItem5.interactable = data.keyItemButtonFive;
        }

        public void SaveData(ref GameData data)
        {
            data.keyItemButtonOne = keyItem1.interactable;
            data.keyItemButtonTwo = keyItem2.interactable;
            data.keyItemButtonThree = keyItem3.interactable;
            data.keyItemButtonFour = keyItem4.interactable;
            data.keyItemButtonFive = keyItem5.interactable;
        }
    }
}

