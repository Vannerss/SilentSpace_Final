namespace SilentSpace.Alien.StateMachine.States
{
    public class AlienFollowSoundState : AlienBaseState
    {
        public AlienFollowSoundState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            StateName = "Follow Sound State";
            InitializeSubState();
            IsRootState = true;
        }
        public override void EnterState() { }
        public override void FixedUpdateState() { }
        public override void UpdateState() { }
        public override void ExitState() { }
        public override void InitializeSubState() { }
        public override void CheckSwitchStates() { }
    }
}
