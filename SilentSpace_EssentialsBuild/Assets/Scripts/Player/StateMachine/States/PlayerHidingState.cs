namespace SilentSpace.Player.StateMachine.States
{
    public class PlayerHidingState : PlayerBaseState
    {
        public PlayerHidingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            _stateName = "Hiding State";
        }
        public override void EnterState() { }
        public override void FixedUpdateState() { }
        public override void UpdateState() { }

        public override void ExitState() { }

        public override void InitializeSubstate() { }

        public override void CheckSwitchStates() { }
    }
}
