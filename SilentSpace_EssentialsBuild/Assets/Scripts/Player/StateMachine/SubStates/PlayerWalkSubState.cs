using SilentSpace.Audio;

namespace SilentSpace.Player.StateMachine.SubStates
{
    [System.Serializable]
    public class PlayerWalkSubState : PlayerBaseState
    {
        public PlayerWalkSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            _stateName = "Walk SubState";
        }
        public override void EnterState()
        {
            _ctx.Log("Entered Walk SubState");
            HandleWalk();
            _ctx.Audio.PlayAudio(AudioType.SFX_Player_Walk);
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            _ctx.Log("Exited Walk SubState");
            _ctx.Audio.StopAudio(AudioType.SFX_Player_Walk);
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Run());
            }
            if (_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(_factory.Crouch());
            }        
            if (_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.CrouchWalk());
            }
            if (!_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(_factory.Empty());
            }
        }

        private void HandleWalk()
        {
            _ctx.Speed = _ctx.walkSpeed;
            //_ctx.Audio.PlayAudio(AudioType.SFX_Sonar_01, false, false, 0.5f);
        }
    }
}
