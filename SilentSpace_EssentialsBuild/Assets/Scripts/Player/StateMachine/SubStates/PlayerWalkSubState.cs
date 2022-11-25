using SilentSpace.Audio;

namespace SilentSpace.Player.StateMachine.SubStates
{
    [System.Serializable]
    public class PlayerWalkSubState : PlayerBaseState
    {
        public PlayerWalkSubState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            StateName = "Walk SubState";
        }
        public override void EnterState()
        {
            Ctx.Log("Entered Walk SubState");
            HandleWalk();
            Ctx.Audio.PlayAudio(AudioType.SFX_Player_Walk);
        }
        public override void FixedUpdateState() { }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        {
            Ctx.Log("Exited Walk SubState");
            Ctx.Audio.StopAudio(AudioType.SFX_Player_Walk);
        }
        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            if (Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.Run());
            }
            if (Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(Factory.Crouch());
            }        
            if (Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude >= 0.1f)
            {
                SwitchState(Factory.CrouchWalk());
            }
            if (!Ctx.IsCrouchingPressed && Ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(Factory.Empty());
            }
        }

        private void HandleWalk()
        {
            Ctx.Speed = Ctx.walkSpeed;
            //_ctx.Audio.PlayAudio(AudioType.SFX_Sonar_01, false, false, 0.5f);
        }
    }
}
