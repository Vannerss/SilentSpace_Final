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

        }

        public override void FixedUpdateState()
        {
 
        }

        public override void UpdateState()
        {

        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchStates()
        {
            
        }

        public override void InitializeSubState()
        {
            
        }
    }
}
