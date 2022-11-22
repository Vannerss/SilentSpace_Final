namespace SilentSpace.Alien.StateMachine.States
{
    public class AlienEatState : AlienBaseState
    {
        public AlienEatState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckSwitchStates()
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeSubState()
        {
            throw new System.NotImplementedException();
        }
    }
}
