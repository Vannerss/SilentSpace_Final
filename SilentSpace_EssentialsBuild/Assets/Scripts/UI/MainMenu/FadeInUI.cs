using UnityEngine;

namespace SilentSpace.UI.MainMenu
{
    public class FadeInUI : MonoBehaviour
    {

        private CanvasGroup _uiGroup;
        private bool _fadeIn;
        void Start()
        {
            _uiGroup = this.GetComponent<CanvasGroup>();
            _uiGroup.alpha = 0;
        }
        void Update()
        {
            if (_uiGroup.alpha < 1 && !_fadeIn)
            {
                
                _uiGroup.alpha += 0.002f;
                if (_uiGroup.alpha >= 1)
                {
                    _fadeIn = true;
                }
            }
        }
    }
}
