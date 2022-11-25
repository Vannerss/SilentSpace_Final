using UnityEngine;
using SilentSpace.Helpers;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerRunSubState : PlayerBaseState
    {
        public PlayerRunSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            StateName = "Run SubState";
        }
    
        public override void EnterState()
        {
            Ctx.Log("Run SubState");
            HandleRun();
            Ctx.Audio.PlayAudio(AudioType.SFX_Player_Run);
            Ctx.timer = new Timer(5f);
            Ctx.timer.OnTimerEnd += OnRanMaxTimeReached;
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            Ctx.timer.Tick();
            CheckSwitchStates();
            if (Ctx.playerManager.oxygenLevel <= 0)
            {
                Ctx.playerManager.oxygenLevel = 0;
                return;
            }
            else 
            {
                Ctx.playerManager.oxygenLevel -= 0.1f;
            }
        }
        public override void ExitState()
        {
            Ctx.Log("Exited Run SubState");
            Ctx.Audio.StopAudio(AudioType.SFX_Player_Run);
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (!Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed)
            {
                SwitchState(Factory.Walk());
            }
            if (Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(Factory.Crouch());
            }        
            if (Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.CrouchWalk());
            }
            if (!Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0.1f)
            {
                SwitchState(Factory.Empty());
            }
        }

        private void HandleRun()
        {
            Ctx.Speed = Ctx.runSpeed;
        }

        private void OnRanMaxTimeReached()
        {
            Ctx.timer.OnTimerEnd -= OnRanMaxTimeReached;
            Ctx.RunOnCooldown();
        }
    }
}
