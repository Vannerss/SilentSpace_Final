using SilentSpace.Alien.StateMachine.States;
using SilentSpace.Alien.StateMachine.SubStates;

namespace SilentSpace.Alien.StateMachine
{
    public class AlienStateFactory
    {
        private readonly AlienStateMachine _context;

        public AlienStateFactory(AlienStateMachine currentContext)
        {
            _context = currentContext;
        }
        public AlienBaseState Roam()
        {
            return new AlienRoamState(_context, this);
        }

        public AlienBaseState Chase()
        {
            return new AlienChaseState(_context, this);
        }

        public AlienBaseState Intimidate()
        {
            return new AlienIntimidateState(_context, this);
        }
    
        public AlienBaseState Attack()
        {
            return new AlienAttackState(_context, this);
        }

        public AlienBaseState FollowSound()
        {
            return new AlienFollowSoundState(_context, this);
        }

        public AlienBaseState AreaRoam()
        {
            return new AlienAreaRoamSubState(_context, this);
        }

        public AlienBaseState NearbyRoam()
        {
            return new AlienNearbyRoamSubState(_context, this);
        }

        public AlienBaseState Empty()
        {
            return new AlienEmptySubState(_context, this);
        }
    }
}