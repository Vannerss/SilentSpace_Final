using UnityEngine;

namespace SilentSpace.Alien.StateMachine.States
{
    public class AlienAttackState : AlienBaseState
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        public AlienAttackState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(
            currentContext, alienStateFactory)
        {
            StateName = "Attack State";
            InitializeSubState();
            IsRootState = true;
        }

        public override void EnterState()
        {
            Ctx.Log(StateName);
            Ctx.animator.SetTrigger(Attack);
            Ctx.agent.SetDestination(Ctx.Position);
        }

        public override void FixedUpdateState()
        {
        }

        public override void UpdateState()
        {
            if (!(Ctx.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f))
            {
                CheckSwitchStates();
            }
        }

        public override void ExitState()
        {
            //_ctx.animator.ResetTrigger("Attack");
        }

        public override void InitializeSubState()
        {
            
        }

        public override void CheckSwitchStates()
        {

            SwitchState(Factory.Chase());
        }

        private void ChangeStateOnTimerEnd()
        {
            //change state to specific state;
        }
    }
}
