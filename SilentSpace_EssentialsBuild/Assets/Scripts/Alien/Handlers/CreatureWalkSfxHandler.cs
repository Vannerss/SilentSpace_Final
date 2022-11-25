using SilentSpace.Audio;
using UnityEngine;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Alien.Handlers
{
    public class CreatureWalkSfxHandler : MonoBehaviour
    {
        private AudioController _audio;

        private void Start()
        {
            _audio = AudioController.Instance;
        }

        /// <summary>
        /// Triggered by animation.
        /// </summary>
        public void PlayStep()
        {
            _audio.PlayAudio(AudioType.SFX_Enemy_Creature_Step);
        }
    }
}
