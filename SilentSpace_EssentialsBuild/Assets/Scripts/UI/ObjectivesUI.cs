using TMPro;
using UnityEngine;

namespace SilentSpace.UI
{
    public class ObjectivesUI : MonoBehaviour
    {
        private TMP_Text _hintTextBlock;

        public Hints[] hintsArray = new Hints[5];

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
    }
}

