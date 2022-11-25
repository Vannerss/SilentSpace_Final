
namespace SilentSpace.Alien.StateMachine
{
    public abstract class AlienBaseState
    {
        protected bool IsRootState = false;
        protected readonly AlienStateMachine Ctx;
        protected readonly AlienStateFactory Factory;
        private AlienBaseState _currentSuperState;
        private AlienBaseState _currentSubState;
        protected string StateName;
        
        public AlienBaseState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        {
            Ctx = currentContext;
            Factory = alienStateFactory;
        }

        public abstract void EnterState();
        public abstract void FixedUpdateState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
            if (_currentSubState != null)
            {
                _currentSubState.UpdateStates();
            }
        }

        protected void SwitchState(AlienBaseState newState)
        {
            ExitState(); //Run exit state before enter the new state.

            newState.EnterState(); //Enter new state.

            //statement to not allow sub-state to change states.
            if (IsRootState)
            {
                Ctx.CurrentState = newState;
            }
            else if (_currentSuperState != null)
            {
                _currentSuperState.SetSubState(newState);
            }
        }
        private void SetSuperState(AlienBaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        protected void SetSubState(AlienBaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}
