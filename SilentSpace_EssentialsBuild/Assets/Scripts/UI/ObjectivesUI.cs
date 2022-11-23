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
        public GameObject keyObject1;
        public GameObject keyObject2;
        public GameObject keyObject3;
        public GameObject keyObject4;
        public GameObject keyObject5;
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
            keyItem1.interactable = data.journal.keyItemOne;
            keyItem2.interactable = data.journal.keyItemOne;
            keyItem3.interactable = data.journal.keyItemOne;
            keyItem4.interactable = data.journal.keyItemOne;
            keyItem5.interactable = data.journal.keyItemOne;

            keyObject1.SetActive(data.scene.keyItemOne);
            keyObject2.SetActive(data.scene.keyItemTwo);
            keyObject3.SetActive(data.scene.keyItemThree);
            keyObject4.SetActive(data.scene.keyItemFour);
            keyObject5.SetActive(data.scene.keyItemFive);
        }

        public void SaveData(ref GameData data)
        {
            data.journal.keyItemOne = keyItem1.interactable;
            data.journal.keyItemTwo = keyItem1.interactable;
            data.journal.keyItemThree = keyItem1.interactable;
            data.journal.keyItemFour = keyItem1.interactable;
            data.journal.keyItemFive = keyItem1.interactable;

            data.journal.keyItemOne = keyObject1.activeSelf;
            data.journal.keyItemTwo = keyObject2.activeSelf;
            data.journal.keyItemThree = keyObject3.activeSelf;
            data.journal.keyItemFour = keyObject4.activeSelf;
            data.journal.keyItemFive = keyObject5.activeSelf;

        }
    }
}

