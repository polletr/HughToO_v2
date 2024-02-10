using UnityEngine;

namespace HughTo0
{
    public abstract class PlayerState
    {
        public Player player { get; set;}

        public Vector2 direction;
        public Vector2 velocity;
        public bool isJumping = false;


        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void StateFixedUpdate() { }
        public virtual void StateUpdate() { }

        #region Player Actions 

        public virtual void OnMovement(Vector2 movement) 
        {
            direction = movement;
        }
        public virtual void OnGlid() { }
        public virtual void OnAttack() { }
        public virtual void OnDash() { }
        public virtual void OnFalling() { }


        public virtual void OnWaterForm() { }
        public virtual void OnIceForm() { }
        public virtual void OnWindForm() { }


        #endregion
        #region Player Checks
        public bool GroundCheck()
        {
            return Physics2D.OverlapCircle(player.GroundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        }

        public void HandleDirection()
        {
            if (direction.x == 0)
            {
                var deceleration = GroundCheck()? player.stats.GroundDeceleration : player.stats.AirDeceleration;
                velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, direction.x * player.stats.MaxSpeed, player.stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion
    }

    public enum PlayerStateType
    {
        Movement,
        Jump,
        Glid,
        Attack,
        Dash
    }
}