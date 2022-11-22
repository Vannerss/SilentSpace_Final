namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerEmptySubState : PlayerBaseState
    {
        public PlayerEmptySubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            _stateName = "Empty SubState";
        }
        public override void EnterState() 
        {
            _ctx.Log("Entered Empty SubState"); 
        }
        public override void ExitState()
        {
            _ctx.Log("Exited Empty SubState");
        }
        public override void FixedUpdateState() { }
        public override void InitializeSubstate() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void CheckSwitchStates()
        {
            if (!_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Walk());
            }

            if (_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Run());
            }

            if (_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.CrouchWalk());
            }

            if (_ctx.IsCrouchingPressed && !_ctx.IsRunningPressed && _ctx.MoveInput.magnitude <= 0.1f)
            {
                SwitchState(_factory.Crouch());
            }
        }
    }
}
