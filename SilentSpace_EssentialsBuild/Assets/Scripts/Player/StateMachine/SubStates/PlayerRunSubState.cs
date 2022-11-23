using UnityEngine;
using SilentSpace.Helpers;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerRunSubState : PlayerBaseState
    {
        public PlayerRunSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            _stateName = "Run SubState";
        }
    
        public override void EnterState()
        {
            _ctx.Log("Run SubState");
            HandleRun();
            _ctx.Audio.PlayAudio(AudioType.SFX_Player_Run);
            _ctx.timer = new Timer(5f);
            _ctx.timer.OnTimerEnd += OnRanMaxTimeReached;
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            _ctx.timer.Tick();
            CheckSwitchStates();
            if (_ctx.playerManager.oxygenLevel <= 0)
            {
                _ctx.playerManager.oxygenLevel = 0;
                return;
            }
            else 
            {
                _ctx.playerManager.oxygenLevel -= 0.1f;
            }
        }
        public override void ExitState()
        {
            _ctx.Log("Exited Run SubState");
            _ctx.Audio.StopAudio(AudioType.SFX_Player_Run);
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (!_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed)
            {
                SwitchState(_factory.Walk());
            }
            if (_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(_factory.Crouch());
            }        
            if (_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.CrouchWalk());
            }
            if (!_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0.1f)
            {
                SwitchState(_factory.Empty());
            }
        }

        private void HandleRun()
        {
            _ctx.Speed = _ctx.runSpeed;
        }

        private void OnRanMaxTimeReached()
        {
            _ctx.timer.OnTimerEnd -= OnRanMaxTimeReached;
            _ctx.RunOnCooldown();
        }
    }
}
