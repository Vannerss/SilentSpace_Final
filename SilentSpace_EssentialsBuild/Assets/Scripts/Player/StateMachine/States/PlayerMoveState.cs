using UnityEngine;

namespace SilentSpace.Player.StateMachine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        public string stateName = "Move State";
        public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            InitializeSubstate();
            IsRootState = true;
        }

        public override void EnterState()
        {
            Ctx.Log("Entered Move State");
        }
        public override void FixedUpdateState()
        {
            MovePlayer();
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            SpeedControl();
        }

        public override void ExitState()
        {
            Ctx.Log("Exited Move State");
        }

        public override void InitializeSubstate()
        {
            if (!Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed)
            {
                SetSubState(Factory.Walk());
            }

            if (Ctx.IsRunningPressed && !Ctx.IsCrouchingPressed)
            {
                SetSubState(Factory.Run());
            }

            if (Ctx.IsCrouchingPressed)
            {
                SetSubState(Factory.CrouchWalk());
            }
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(Factory.Idle());
            }
        }

        public void MovePlayer()
        {
            Ctx.MoveDirection = Ctx.orientation.forward * Ctx.MoveInput.y + Ctx.orientation.right * Ctx.MoveInput.x;

            Ctx.PlayerRigidbody.AddForce(Ctx.MoveDirection.normalized * (Ctx.Speed * 10f), ForceMode.Force);

            if (OnSlope())
            {
                Ctx.PlayerRigidbody.AddForce(GetSlopeMoveDirection() * (Ctx.Speed * 20f), ForceMode.Force);

                if (Ctx.PlayerRigidbody.velocity.y > 0)
                {
                    Ctx.PlayerRigidbody.AddForce(Vector3.down * 80f, ForceMode.Force);
                }
            }

            Ctx.PlayerRigidbody.useGravity = !OnSlope();
        }

        public void SpeedControl()
        {
            if (OnSlope())
            {
                if (Ctx.PlayerRigidbody.velocity.magnitude > Ctx.Speed)
                {
                    Ctx.PlayerRigidbody.velocity = Ctx.PlayerRigidbody.velocity.normalized * Ctx.Speed;
                }
            }
            else
            {
                var velocity = Ctx.PlayerRigidbody.velocity;
                var flatVelocity = new Vector3(velocity.x, 0f, velocity.z);

                if (!(flatVelocity.magnitude > Ctx.Speed)) return;
                var limitedVelocity = flatVelocity.normalized * Ctx.Speed;
                Ctx.PlayerRigidbody.velocity = new Vector3(limitedVelocity.x, Ctx.PlayerRigidbody.velocity.y, limitedVelocity.z);
            }
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(Ctx.transform.position, Vector3.down, out Ctx.SlopeHit, Ctx.PlayerHeight * 0.5f + 0.3f))
            {
                var angle = Vector3.Angle(Vector3.up, Ctx.SlopeHit.normal);
                return angle < Ctx.maxSlopeAngle && angle != 0;
            }
            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(Ctx.MoveDirection, Ctx.SlopeHit.normal).normalized;
        }
    }
}
