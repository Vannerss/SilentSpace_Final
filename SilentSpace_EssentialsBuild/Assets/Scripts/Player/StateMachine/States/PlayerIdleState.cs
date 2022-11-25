namespace SilentSpace.Player.StateMachine.States
{
    public class PlayerIdleState : PlayerBaseState
    {
    
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            InitializeSubstate();
            IsRootState = true;
            StateName = "Idle State";
        }
        public override void EnterState()
        {
            Ctx.Log("Entered Idle State");
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            Ctx.Log("Exited Idle State");
        }

        public override void InitializeSubstate()
        {
            if(!Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0f)
            {
                SetSubState(Factory.Empty());
            }
            if (Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0f)
            {
                SetSubState(Factory.Crouch());
            }
            if (!Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SetSubState(Factory.Walk());
            }
            if (Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SetSubState(Factory.CrouchWalk());
            }
            if (Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SetSubState(Factory.Run());
            }
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Move());
            }
        }
    }
}
