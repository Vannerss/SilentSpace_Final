using UnityEngine;

namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerCrouchSubState : PlayerBaseState
    {
    
        public PlayerCrouchSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            _stateName = "Crouch SubState";
        }

        public override void EnterState()
        {
            _ctx.Log("Entered Crouch SubState");
            HandleCrouch();
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            _ctx.Log("Exited Crouch SubState");
            HandleUncrouch();
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (!_ctx.IsCrouchingPressed && !_ctx.IsRunningPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Walk());
            }
            if (!_ctx.IsCrouchingPressed && _ctx.IsRunningPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Run());
            }
            if(_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.CrouchWalk());
            }
            if (!_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0.1f)
            {
                SwitchState(_factory.Empty());
            }
        }

        void HandleCrouch()
        {
            _ctx.transform.localScale = new Vector3(_ctx.transform.localScale.x, _ctx.crouchYScale, _ctx.transform.localScale.z);
            _ctx.PlayerRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        void HandleUncrouch()
        {
            _ctx.transform.localScale = new Vector3(_ctx.transform.localScale.x, _ctx.StartYScale, _ctx.transform.localScale.z);
        }
    }
}
