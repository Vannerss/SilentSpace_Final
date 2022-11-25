using UnityEngine;
using AudioType = SilentSpace.Audio;

namespace SilentSpace.Alien.StateMachine.SubStates
{
    public class AlienAreaRoamSubState : AlienBaseState
    {
        private int _index = 0;
        private int _currentAreaIndex;
        private int _previousAreaIndex;
        
        public AlienAreaRoamSubState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            StateName = "Area Roam SubState";
        }

        public override void EnterState()
        {
            Ctx.Log("Entered Area Roam Substate");
            _currentAreaIndex = Ctx.areas.Length - 1;
            if(Ctx.areas.Length != 0) FindNextArea();
            Ctx.audioController.PlayAudio(AudioType.AudioType.SFX_Enemy_Creature_Speak_01,true);
        }
        public override void FixedUpdateState() { }

        public override void UpdateState()
        {
            if(Ctx.areas.Length == 0) return;
            
            if (!(Ctx.agent.remainingDistance <= Ctx.agent.stoppingDistance)) return;
            
            if ((_index + 1) <= Ctx.areas[_currentAreaIndex].transforms.Length)
            {
                Ctx.agent.SetDestination(Ctx.areas[_currentAreaIndex].transforms[_index].position);
                _index++;
            }
            else
            {
                FindNextArea();
                _index = 0;
            }
        }

        public override void ExitState()
        {
            Ctx.Log("Entered Area Roam Substate");
            Ctx.audioController.StopAudio(AudioType.AudioType.SFX_Enemy_Creature_Speak_01, true);
        }
        public override void InitializeSubState() { }
        public override void CheckSwitchStates() { }
        
        /* Find the next sets of transforms to follow (this is random and it runs after the enemy traversed through all the point
        * of an area or its returning to the roaming state.
        */
        private void FindNextArea()
        {
            Ctx.Log("Finding Next Area");
            while (true)
            {
                var random = Random.Range(0, Ctx.areas.Length);
                
                if (random != _currentAreaIndex && random != _previousAreaIndex)
                {
                    _previousAreaIndex = _currentAreaIndex;
                    _currentAreaIndex = random;
                }
                else
                {
                    continue;
                }
                Ctx.Log("Next Area is: Area " + (_currentAreaIndex + 1));
                break;
            }
        }
    }
}
