using UnityEngine;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerCrouchWalkSubState : PlayerBaseState
    {
        public PlayerCrouchWalkSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            _stateName = "Crouch Walk SubState";
        }

        public override void EnterState()
        {
            _ctx.Log("Entered CrouchWalk SubState");
            HandleCrouch();
            _ctx.Audio.PlayAudio(AudioType.SFX_Player_CrouchWalk, false, false, 0.5f);
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            _ctx.Log("Exited CrouchWalk SubState");
            _ctx.Audio.StopAudio(AudioType.SFX_Player_CrouchWalk);
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (!_ctx.IsCrouchingPressed && !_ctx.IsRunningPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Walk());
                HandleUncrouch();
            }
            if (!_ctx.IsCrouchingPressed && _ctx.IsRunningPressed && _ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(_factory.Run());
                HandleUncrouch();
            }
            if(_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0)
            {
                SwitchState(_factory.Crouch());
            }
            if (!_ctx.IsCrouchingPressed && _ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(_factory.Empty());
            }
        }

        void HandleCrouch()
        {
            _ctx.Speed = _ctx.crouchSpeed;
            _ctx.transform.localScale = new Vector3(_ctx.transform.localScale.x, _ctx.crouchYScale, _ctx.transform.localScale.z);
            _ctx.PlayerRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        void HandleUncrouch()
        {
            _ctx.transform.localScale = new Vector3(_ctx.transform.localScale.x, _ctx.StartYScale, _ctx.transform.localScale.z);
        }
    }
}
