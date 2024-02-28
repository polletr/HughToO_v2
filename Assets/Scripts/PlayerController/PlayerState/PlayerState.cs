using UnityEngine;

namespace HughTo0
{
    public abstract class PlayerState
    {
        public Player player { get; set; }
        public InputManager inputManager { get; set; }

        protected PlayerState currentState;

        protected bool _bufferedJumpUsable;
        protected bool _endedJumpEarly;
        protected bool _coyoteUsable;
        protected float _timeJumpWasPressed;

        protected bool exitFromInAir;

        protected bool CanUseCoyote;

        protected Vector2 velocity = new Vector2();

        protected bool HasBufferedJump => (Time.time - inputManager.JumpButtonPressedLast < player.currentStats.JumpBuffer && inputManager.IsJumpHeldDown);//_bufferedJumpUsable && _time < _timeJumpWasPressed + player.currentStats.JumpBuffer;

        public virtual void EnterState() {
            velocity = player._rb.velocity;
        }
        public virtual void ExitState() { }
        public virtual void StateFixedUpdate() 
        {
            player._rb.velocity = velocity + player.ParentVelocity;

        }
        public virtual void StateUpdate() { }
        public virtual void HandleMovement(Vector2 move) { }
        public virtual void HandleJump()
        {

        }

    }

    public enum PlayerStateType
    {
        Movement,
        Grounded,
        InAir,
        Jump,
        Glide,
        Attack,
        Dash
    }
}