using UnityEngine;

namespace HughTo0
{
    public abstract class PlayerState
    {
        public Player player { get; set; }
        public InputManager inputManager { get; set; }

        protected PlayerState currentState;


        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void StateFixedUpdate() { }
        public virtual void StateUpdate() { }
        public virtual void HandleMovement(Vector2 move) { }
        public virtual void HandleJump()
        {
        }

        #region Player Actions 


        /*public virtual void HandleGravity()
        {
            if
            {
                var inAirGravity = player.currentStats.FallAcceleration;
                velocity.y = Mathf.MoveTowards(velocity.y, -player.currentStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
                if (velocity.y < 0)
                {
                    Debug.Log("falling ");
                    //play animation of falling
                }
                else if (velocity.y > 0)
                {
                    //play animation of jumping
                }
            }
        }*/

        #endregion
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