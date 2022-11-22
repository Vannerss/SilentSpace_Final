using SilentSpace.Player.StateMachine.States;
using SilentSpace.Player.StateMachine.SubStates;

namespace SilentSpace.Player.StateMachine
{
    public class PlayerStateFactory
    {
        private PlayerStateMachine _context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
        }

        public PlayerBaseState Idle() 
        {
            return new PlayerIdleState(_context, this);
        }
        public PlayerBaseState Move()
        {
            return new PlayerMoveState(_context, this);
        }
        public PlayerBaseState Walk()
        {
            return new PlayerWalkSubState(_context, this);
        }
        public PlayerBaseState Run()
        {
            return new PlayerRunSubState(_context, this);
        }
        public PlayerBaseState Crouch()
        {
            return new PlayerCrouchSubState(_context, this);
        }
        public PlayerBaseState CrouchWalk()
        {
            return new PlayerCrouchWalkSubState(_context, this);
        }
        public PlayerBaseState Hiding()
        {
            return new PlayerHidingState(_context, this);
        }
        public PlayerBaseState Empty()
        {
            return new PlayerEmptySubState(_context, this);
        }
    }
}

