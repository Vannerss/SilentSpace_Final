namespace SilentSpace.Player.StateMachine.States
{
    public class PlayerIdleState : PlayerBaseState
    {
    
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            InitializeSubstate();
            _isRootState = true;
            _stateName = "Idle State";
        }
        public override void EnterState()
        {
            _ctx.Log("Entered Idle State");
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            _ctx.Log("Exited Idle State");
        }

        public override void InitializeSubstate()
        {
            if(!_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0f)
            {
                SetSubState(_factory.Empty());
            }
            if (_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0f)
            {
                SetSubState(_factory.Crouch());
            }
            if (!_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SetSubState(_factory.Walk());
            }
            if (_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SetSubState(_factory.CrouchWalk());
            }
            if (_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SetSubState(_factory.Run());
            }
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Move());
            }
        }
    }
}
