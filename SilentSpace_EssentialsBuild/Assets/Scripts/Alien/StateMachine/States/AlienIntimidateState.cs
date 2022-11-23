using SilentSpace.Helpers;
using UnityEngine;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Alien.StateMachine.States
{
    public class AlienIntimidateState : AlienBaseState
    {
        private static readonly int Intimidate = Animator.StringToHash("Intimidate");

        public AlienIntimidateState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            StateName = "Intimidate State";
            InitializeSubState();
            IsRootState = true;
        }
        public override void EnterState()
        {
            Ctx.agent.SetDestination(Ctx.Position);
            Ctx.Log(StateName);
            Ctx.Timer = new Timer(3f);
            Ctx.Timer.OnTimerEnd += CheckSwitchStates;
            Ctx.transform.LookAt(Ctx.playerManager.Position);
            Debug.Log("IntimidateState");
            Ctx.animator.SetTrigger(Intimidate);
            Ctx.audioController.PlayAudio(AudioType.SFX_Enemy_Creature_Roar);
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            Ctx.Timer.Tick();
        }

        public override void ExitState()
        {
            Ctx.audioController.StopAudio(AudioType.SFX_Enemy_Creature_Roar);
            Ctx.Timer.OnTimerEnd -= CheckSwitchStates;
        }
        public override void InitializeSubState()
        {
            SetSubState(Factory.Empty());
        }
        public override void CheckSwitchStates()
        {
            SwitchState(Factory.Chase());
        }
    }
}
