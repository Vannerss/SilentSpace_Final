using UnityEngine;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Alien.StateMachine.States
{
    public class AlienRoamState : AlienBaseState
    {
        private static readonly int Roaming = Animator.StringToHash("Roaming");
        private bool _finishedNRoam;
        public AlienRoamState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            StateName = "Roam State";
            InitializeSubState();
            IsRootState = true;
        }
        
        public override void EnterState()
        {
            Ctx.Log(StateName);
            _finishedNRoam = false;
            Ctx.animator.SetTrigger(Roaming);
            Ctx.CurrentStateName = StateName;
            Ctx.audioController.PlayAudio(AudioType.SFX_Enemy_Creature_Speak_01, true);
        }
        
        public override void FixedUpdateState() { }
        
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        
        public override void ExitState()
        {
            Ctx.agent.SetDestination(Ctx.Position);
            Ctx.PreviousStateName = StateName;
            Ctx.audioController.StopAudio(AudioType.SFX_Enemy_Creature_Speak_01, true);
        }
        
        public override void InitializeSubState()
        {
            if (Ctx.PreviousStateName == "Chase State" && !_finishedNRoam)
            {
                SetSubState(Factory.NearbyRoam());
                _finishedNRoam = true;
            }
            else
            {
                SetSubState(Factory.AreaRoam());
            }
        }
        
        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(Ctx.Position, Ctx.playerManager.Position) <= 15f && Ctx.playerManager.CurrentSubState == "Run SubState")
            {
                SwitchToIntimidate();
            }        
            if (Vector3.Distance(Ctx.Position, Ctx.playerManager.Position) <= 7f && Ctx.playerManager.CurrentSubState == "Walk SubState")
            {
                SwitchToIntimidate();
            }        
            if (Vector3.Distance(Ctx.Position, Ctx.playerManager.Position) <= 4f && Ctx.playerManager.CurrentSubState == "Crouch Walk SubState")
            {
                SwitchToIntimidate();
            }
        }

        private void SwitchToIntimidate()
        {
            SwitchState(Factory.Intimidate());
        }
    }
}