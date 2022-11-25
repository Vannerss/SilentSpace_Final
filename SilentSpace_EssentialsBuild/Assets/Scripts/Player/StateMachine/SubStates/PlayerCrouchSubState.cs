using UnityEngine;

namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerCrouchSubState : PlayerBaseState
    {
    
        public PlayerCrouchSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            StateName = "Crouch SubState";
        }

        public override void EnterState()
        {
            Ctx.Log("Entered Crouch SubState");
            HandleCrouch();
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            Ctx.Log("Exited Crouch SubState");
            HandleUncrouch();
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (!Ctx.IsCrouchingPressed && !Ctx.IsRunningPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Walk());
            }
            if (!Ctx.IsCrouchingPressed && Ctx.IsRunningPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Run());
            }
            if(Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.CrouchWalk());
            }
            if (!Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0.1f)
            {
                SwitchState(Factory.Empty());
            }
        }

        void HandleCrouch()
        {
            Ctx.transform.localScale = new Vector3(Ctx.transform.localScale.x, Ctx.crouchYScale, Ctx.transform.localScale.z);
            Ctx.PlayerRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        void HandleUncrouch()
        {
            Ctx.transform.localScale = new Vector3(Ctx.transform.localScale.x, Ctx.StartYScale, Ctx.transform.localScale.z);
        }
    }
}
