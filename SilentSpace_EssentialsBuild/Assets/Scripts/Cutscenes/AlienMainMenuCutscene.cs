using SilentSpace.Audio;
using UnityEngine;
using UnityEngine.AI;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Cutscenes
{
    public class AlienMainMenuCutscene : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private AudioController _audio;
        private bool _startNext;
        private bool _ranOnce;
        private bool _ranOnce1;
        private bool _ranOnce2;
        
        public GameObject ui;
        public Transform destination;
        
        private static readonly int Eat = Animator.StringToHash("Eat");
        
        private void Start()
        {
            _audio = AudioController.Instance;
            _agent = this.GetComponent<NavMeshAgent>();
            _animator = this.GetComponent<Animator>();
            _audio.PlayAudio(AudioType.SFX_Enemy_Creature_Speak_01);
        }

        private void Update()
        {
            if (!_startNext)
            {
                _agent.SetDestination(destination.position);
                if (Vector3.Distance(transform.position, destination.position) <= 3)
                {
                    _startNext = true;
                    _agent.SetDestination(transform.position);
                    _animator.SetTrigger(Eat);
                }
            }
            else 
            {
                transform.LookAt(destination.position);
                if (!_ranOnce)
                {
                    _ranOnce = true;
                    _audio.StopAudio(AudioType.SFX_Enemy_Creature_Speak_01, true);
                } 
                if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Intimidate"))
                {
                    //Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).GetType());
                    if (!_ranOnce1)
                    {
                        _ranOnce1 = true;
                        _audio.PlayAudio(AudioType.SFX_Enemy_Creature_Roar, false, false, 0f);
                    }

                } else if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Eat"))
                {
                    if (!_ranOnce2)
                    {
                        _ranOnce2 = true;
                        _audio.StopAudio(AudioType.SFX_Enemy_Creature_Roar, true);
                        _animator.SetTrigger(Eat);
                        ui.SetActive(true);
                    }
                }
            }
        }
    }
}
