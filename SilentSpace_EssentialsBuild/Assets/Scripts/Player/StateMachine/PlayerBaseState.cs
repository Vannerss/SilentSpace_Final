
namespace SilentSpace.Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        protected bool IsRootState = false;
        protected string StateName;
        protected readonly PlayerStateMachine Ctx;
        protected readonly PlayerStateFactory Factory;
        private PlayerBaseState _currentSuperState;
        private PlayerBaseState _currentSubState;

        protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            Ctx = currentContext;
            Factory = playerStateFactory;
        }

        public abstract void EnterState();
        public abstract void FixedUpdateState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubstate();

        public void UpdateStates()
        {
            UpdateState();
            if(_currentSubState != null)
            {
                _currentSubState.UpdateStates();
            }
        }
    
        protected void SwitchState(PlayerBaseState newState) {
            ExitState(); //Run exit state before enter the new state.

            newState.EnterState(); //Enter new state.

            // _ctx.CurrentState = newState;
            //statement to not allow substate to change states.
            if (IsRootState)
            {
                Ctx.CurrentState = newState;
                Ctx.CurrentStateName = newState.StateName;
            } 
            else if(_currentSuperState != null)
            {
                _currentSuperState.SetSubState(newState);
                Ctx.CurrentSubState = newState;
                Ctx.CurrentSubStateName = newState.StateName;
            }
        
        }
        
        private void SetSuperState(PlayerBaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        
        protected void SetSubState(PlayerBaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}
