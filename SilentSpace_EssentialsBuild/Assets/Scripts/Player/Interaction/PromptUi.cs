
using TMPro;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class PromptUi : MonoBehaviour
    {
        private Camera _mainCam;
        [SerializeField] private GameObject _uiPanel;
        [SerializeField] TextMeshProUGUI _promptText;
        private void Start()
        {
            _mainCam = Camera.main;
            _uiPanel.SetActive(false);
        }

        private void LateUpdate()
        {
            var rotation = _mainCam.transform.rotation;
            transform.LookAt(transform.position + rotation*Vector3.forward, rotation * Vector3.up);
        }

        public bool IsDisplayed = false;
        
        public void SetUp(string promptText)
        {
            _promptText.text = promptText;
            _uiPanel.SetActive(true);
            IsDisplayed = true;
        }

        public void Close()
        {
            _uiPanel.SetActive(false);
            IsDisplayed = false;
        }
    }
}
