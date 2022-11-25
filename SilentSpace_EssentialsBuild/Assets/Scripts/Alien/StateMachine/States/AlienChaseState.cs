using SilentSpace.Helpers;
using UnityEngine;

namespace SilentSpace.Alien.StateMachine.States
{
    public sealed class AlienChaseState : AlienBaseState
    {
        private static readonly int Chasing = Animator.StringToHash("Chasing");

        public AlienChaseState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            StateName = "Chase State";
            InitializeSubState();
            IsRootState = true;
        }

        public override void EnterState()
        {
           Ctx.Log(StateName);
           Ctx.Timer = new Timer();
           Ctx.CurrentStateName = StateName;
           Ctx.animator.SetTrigger(Chasing);
           Ctx.Timer.OnTimerEnd += SwitchToRoam;
        }

        public override void FixedUpdateState() { }

        public override void UpdateState()
        {
            CheckSwitchStates();
            if(Vector3.Distance(Ctx.Position, Ctx.playerManager.Position) > 4.5f) Ctx.agent.SetDestination(Ctx.playerManager.Position);
            if (Ctx.timerIsRunning)
            {
                Ctx.Timer.Tick();
            } 
        }

        public override void ExitState()
        {
            Ctx.PreviousStateName = StateName;
            Ctx.agent.SetDestination(Ctx.Position);
            Ctx.Timer.OnTimerEnd -= SwitchToRoam;
        }

        public override void InitializeSubState() { }

        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(Ctx.Position, Ctx.playerManager.Position) <= 5f)
            {
                Ctx.agent.SetDestination(Ctx.Position);
                SwitchState(Factory.Attack());
            }

            if(Vector3.Distance(Ctx.Position, Ctx.playerManager.Position) >= 25f)
            {
                if (Ctx.timerIsRunning) return;
                Ctx.Timer.RemainingSeconds = 3f;
                Ctx.timerIsRunning = true;
            }
            else
            {
                Ctx.timerIsRunning = false;
            }
        }

        private void SwitchToRoam()
        {
            SwitchState(Factory.Roam());
        }
    }
}
