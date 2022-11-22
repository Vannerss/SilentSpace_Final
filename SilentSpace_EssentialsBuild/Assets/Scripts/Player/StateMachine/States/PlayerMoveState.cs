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
            _isRootState = true;
        }

        public override void EnterState()
        {
            _ctx.Log("Entered Move State");
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
            _ctx.Log("Exited Move State");
        }

        public override void InitializeSubstate()
        {
            if (!_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed)
            {
                SetSubState(_factory.Walk());
            }

            if (_ctx.IsRunningPressed && !_ctx.IsCrouchingPressed)
            {
                SetSubState(_factory.Run());
            }

            if (_ctx.IsCrouchingPressed)
            {
                SetSubState(_factory.CrouchWalk());
            }
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.MoveInput.magnitude <= 0f)
            {
                SwitchState(_factory.Idle());
            }
        }

        public void MovePlayer()
        {
            _ctx.MoveDirection = _ctx.orientation.forward * _ctx.MoveInput.y + _ctx.orientation.right * _ctx.MoveInput.x;

            _ctx.PlayerRigidbody.AddForce(_ctx.MoveDirection.normalized * _ctx.Speed * 10f, ForceMode.Force);

            if (OnSlope())
            {
                _ctx.PlayerRigidbody.AddForce(GetSlopeMoveDirection() * _ctx.Speed * 20f, ForceMode.Force);

                if (_ctx.PlayerRigidbody.velocity.y > 0)
                {
                    _ctx.PlayerRigidbody.AddForce(Vector3.down * 80f, ForceMode.Force);
                }
            }

            _ctx.PlayerRigidbody.useGravity = !OnSlope();
        }

        public void SpeedControl()
        {
            if (OnSlope())
            {
                if (_ctx.PlayerRigidbody.velocity.magnitude > _ctx.Speed)
                {
                    _ctx.PlayerRigidbody.velocity = _ctx.PlayerRigidbody.velocity.normalized * _ctx.Speed;
                }
            }
            else
            {
                Vector3 flatVelocity = new Vector3(_ctx.PlayerRigidbody.velocity.x, 0f, _ctx.PlayerRigidbody.velocity.z);

                if (flatVelocity.magnitude > _ctx.Speed)
                {
                    Vector3 limitedVelocity = flatVelocity.normalized * _ctx.Speed;
                    _ctx.PlayerRigidbody.velocity = new Vector3(limitedVelocity.x, _ctx.PlayerRigidbody.velocity.y, limitedVelocity.z);
                }
            }
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(_ctx.transform.position, Vector3.down, out _ctx.SlopeHit, _ctx.PlayerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, _ctx.SlopeHit.normal);
                return angle < _ctx.maxSlopeAngle && angle != 0;
            }
            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(_ctx.MoveDirection, _ctx.SlopeHit.normal).normalized;
        }
    }
}
