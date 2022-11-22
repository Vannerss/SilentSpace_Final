
namespace SilentSpace.Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        protected bool _isRootState = false;
        protected string _stateName;
        protected PlayerStateMachine _ctx;
        protected PlayerStateFactory _factory;
        protected PlayerBaseState _currentSuperState;
        protected PlayerBaseState _currentSubState;
        public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            _ctx = currentContext;
            _factory = playerStateFactory;
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
            if (_isRootState)
            {
                _ctx.CurrentState = newState;
                _ctx.CurrentStateName = newState._stateName;
            } 
            else if(_currentSuperState != null)
            {
                _currentSuperState.SetSubState(newState);
                _ctx.CurrentSubState = newState;
                _ctx.CurrentSubStateName = newState._stateName;
            }
        
        }
        protected void SetSuperState(PlayerBaseState newSuperState)
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
