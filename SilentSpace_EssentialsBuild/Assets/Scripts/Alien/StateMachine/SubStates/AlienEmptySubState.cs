namespace SilentSpace.Alien.StateMachine.SubStates
{
    public class AlienEmptySubState : AlienBaseState
    {
        public AlienEmptySubState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            StateName = "Empty SubState";
        }
        public override void EnterState() { }
        public override void FixedUpdateState() { }
        public override void UpdateState() { }
        public override void ExitState() { }
        public override void InitializeSubState() { }
        public override void CheckSwitchStates() { }
    }
}
