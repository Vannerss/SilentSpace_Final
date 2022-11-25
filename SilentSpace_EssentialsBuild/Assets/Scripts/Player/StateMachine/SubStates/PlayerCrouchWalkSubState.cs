using UnityEngine;
using AudioType = SilentSpace.Audio.AudioType;

namespace SilentSpace.Player.StateMachine.SubStates
{
    public class PlayerCrouchWalkSubState : PlayerBaseState
    {
        public PlayerCrouchWalkSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            StateName = "Crouch Walk SubState";
        }

        public override void EnterState()
        {
            Ctx.Log("Entered CrouchWalk SubState");
            HandleCrouch();
            Ctx.Audio.PlayAudio(AudioType.SFX_Player_CrouchWalk, false, false, 0.5f);
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            Ctx.Log("Exited CrouchWalk SubState");
            Ctx.Audio.StopAudio(AudioType.SFX_Player_CrouchWalk);
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (!Ctx.IsCrouchingPressed && !Ctx.IsRunningPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Walk());
                HandleUncrouch();
            }
            if (!Ctx.IsCrouchingPressed && Ctx.IsRunningPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Run());
                HandleUncrouch();
            }
            if(Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0)
            {
                SwitchState(Factory.Crouch());
            }
            if (!Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(Factory.Empty());
            }
        }

        void HandleCrouch()
        {
            Ctx.Speed = Ctx.crouchSpeed;
            Ctx.transform.localScale = new Vector3(Ctx.transform.localScale.x, Ctx.crouchYScale, Ctx.transform.localScale.z);
            Ctx.PlayerRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        void HandleUncrouch()
        {
            Ctx.transform.localScale = new Vector3(Ctx.transform.localScale.x, Ctx.StartYScale, Ctx.transform.localScale.z);
        }
    }
}
