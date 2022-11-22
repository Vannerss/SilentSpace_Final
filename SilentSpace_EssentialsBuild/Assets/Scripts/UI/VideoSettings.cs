using System.Collections.Generic;
using SilentSpace.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.UI
{
    public class VideoSettings : MonoBehaviour
    {
        private AudioController _audioController;
        private int _selectedResolution;

        public Toggle fsToggle, vsyncToggle;
        public List<ResItem> resolutions = new List<ResItem>();
        public TMP_Text resolutionLabel;


        private void Start()
        {
            _audioController = AudioController.Instance;
            fsToggle.isOn = Screen.fullScreen;

            vsyncToggle.isOn = QualitySettings.vSyncCount != 0;

            var foundRes = false;
            for(var i = 0; i < resolutions.Count; i++)
            {
                if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
                {
                    foundRes = true;

                    _selectedResolution = i;

                    UpdateResLabel();
                }
            }

            if (!foundRes)
            {
                ResItem newRes = new ResItem();
                newRes.horizontal = Screen.width;
                newRes.vertical = Screen.height;

                resolutions.Add(newRes);
                _selectedResolution = resolutions.Count - 1;

                UpdateResLabel();
            }
            
            this.gameObject.SetActive(false);
        }
        
        public void ResLeft() 
        {
            _selectedResolution--;
            if(_selectedResolution < 0)
            {
                _selectedResolution = 0;
            }
            UpdateResLabel();
        }

        public void ResRight()
        {
            _selectedResolution++;
            if (_selectedResolution > resolutions.Count - 1)
            {
                _selectedResolution = resolutions.Count - 1;
            }
            UpdateResLabel();
        }

        private void UpdateResLabel()
        {
            resolutionLabel.text = resolutions[_selectedResolution].horizontal.ToString() + " X " + resolutions[_selectedResolution].vertical.ToString();
        }

        public void ApplyGraphics()
        {
            if (vsyncToggle.isOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }

            Screen.SetResolution(resolutions[_selectedResolution].horizontal, resolutions[_selectedResolution].vertical, fsToggle.isOn);
        }
    }

    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }
}