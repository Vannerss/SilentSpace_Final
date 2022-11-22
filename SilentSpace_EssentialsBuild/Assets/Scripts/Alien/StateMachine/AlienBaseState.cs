
namespace SilentSpace.Alien.StateMachine
{
    public abstract class AlienBaseState
    {
        protected bool IsRootState = false;
        protected AlienStateMachine Ctx;
        protected AlienStateFactory Factory;
        protected AlienBaseState CurrentSuperState;
        protected AlienBaseState CurrentSubState;
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
            if (CurrentSubState != null)
            {
                CurrentSubState.UpdateStates();
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
            else if (CurrentSuperState != null)
            {
                CurrentSuperState.SetSubState(newState);
            }
        }
        protected void SetSuperState(AlienBaseState newSuperState)
        {
            CurrentSuperState = newSuperState;
        }
        protected void SetSubState(AlienBaseState newSubState)
        {
            CurrentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}
