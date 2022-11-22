using SilentSpace.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.UI
{
    public class AudioSettings : MonoBehaviour
    {
        private AudioController _audioController;
        
        public AudioMixer audioMixer;
        public TMP_Text masterLabel, musicLabel, sfxLabel, uiSfxLabel;
        public Slider masterSlider, musicSlider, sfxSlider, uiSfxSlider;

        private void Start()
        {
            _audioController = AudioController.Instance;

            if (PlayerPrefs.HasKey("MasterVol"))
            {
                audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            }
            if (PlayerPrefs.HasKey("MusicVol"))
            {
                audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            }
            if (PlayerPrefs.HasKey("SFXVol"))
            {
                audioMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            }        
            if (PlayerPrefs.HasKey("UIVol"))
            {
                audioMixer.SetFloat("UIVol", PlayerPrefs.GetFloat("UIVol"));
            }

            audioMixer.GetFloat("MasterVol", out var vol);
            masterSlider.value = vol;        
            audioMixer.GetFloat("MusicVol", out vol);
            musicSlider.value = vol;        
            audioMixer.GetFloat("SFXVol", out vol);
            sfxSlider.value = vol;        
            audioMixer.GetFloat("UIVol", out vol);
            uiSfxSlider.value = vol;

            masterLabel.text = Mathf.RoundToInt(((masterSlider.value + 80) / 80) * 100).ToString();
            musicLabel.text = Mathf.RoundToInt(((musicSlider.value + 80) / 80) * 100).ToString();
            sfxLabel.text = Mathf.RoundToInt(((sfxSlider.value + 80) / 80) * 100).ToString();
            uiSfxLabel.text = Mathf.RoundToInt(((uiSfxSlider.value + 80) / 80) * 100).ToString();
            
            SetFalse();
        }

        private void SetFalse()
        {
            this.gameObject.SetActive(false);
        }
        public void PlaySliderClick()
        {
            _audioController.PlayAudio(AudioType.SFX_UI_Clack_Slider);
        }

        public void SetMasterVolume(float val)
        {
        
            masterLabel.text = Mathf.RoundToInt(((val + 80) / 80) * 100).ToString();

            audioMixer.SetFloat("MasterVol", val);
            PlayerPrefs.SetFloat("MasterVol", val);
        }

        public void SetMusicVolume(float val)
        {
            musicLabel.text = Mathf.RoundToInt(((val + 80) / 80) * 100).ToString();

            audioMixer.SetFloat("MusicVol", val);
            PlayerPrefs.SetFloat("MusicVol", val);
        }

        public void SetSfxVolume(float val)
        {
            sfxLabel.text = Mathf.RoundToInt(((val + 80) / 80) * 100).ToString();

            audioMixer.SetFloat("SFXVol", val);
            PlayerPrefs.SetFloat("SFXVol", val);
        }
        public void SetUisfxVolume(float val)
        {
            uiSfxLabel.text = Mathf.RoundToInt(((val + 80) / 80) * 100).ToString();

            audioMixer.SetFloat("UIVol", val);
            PlayerPrefs.SetFloat("UIVol", val);
        }
    }
}
