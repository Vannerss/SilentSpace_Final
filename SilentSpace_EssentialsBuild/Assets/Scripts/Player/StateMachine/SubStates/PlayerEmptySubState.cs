namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerEmptySubState : PlayerBaseState
    {
        public PlayerEmptySubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            StateName = "Empty SubState";
        }
        public override void EnterState() 
        {
            Ctx.Log("Entered Empty SubState"); 
        }
        public override void ExitState()
        {
            Ctx.Log("Exited Empty SubState");
        }
        public override void FixedUpdateState() { }
        public override void InitializeSubstate() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void CheckSwitchStates()
        {
            if (!Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Walk());
            }

            if (Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Run());
            }

            if (Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.CrouchWalk());
            }

            if (Ctx.IsCrouchingPressed && !Ctx.IsRunningPressed && Ctx.MoveInput.magnitude <= 0.1f)
            {
                SwitchState(Factory.Crouch());
            }
        }
    }
}
