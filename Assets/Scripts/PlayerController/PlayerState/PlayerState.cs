using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace HughTo0
{
    public abstract class PlayerState
    {
        public Player player { get; set; }
        public InputManager inputManager { get; set; }

        public Vector2 velocity;


        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void StateFixedUpdate()
        {
            OnMovement(inputManager.Movement);
        }
        public virtual void StateUpdate() { }

        #region Player Actions 

        public virtual void OnMovement(Vector2 movement)
        {
            if (movement.x == 0)
            {
                var deceleration = player.GroundCheck() ? player.currentStats.GroundDeceleration : player.currentStats.AirDeceleration;
                velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, movement.x * player.currentStats.MaxSpeed, player.currentStats.Acceleration * Time.fixedDeltaTime);

            }

            if ((movement.x > 0f && player.transform.localScale.x < 0) || (movement.x < 0f && player.transform.localScale.x > 0))
            {
                Vector3 localScale = player.transform.localScale;
                localScale.x *= -1f;
                player.transform.localScale = localScale;
            }

            HandleGravity();
            player.Velocity = velocity;
            

        }
        public virtual void OnGlid() { }
        public virtual void OnAttack() { }
        public virtual void OnDash() { }
        public virtual void OnFalling() { }

        public virtual void HandleGravity()
        {
            if (player.GroundCheck() && velocity.y <= 0f)
            {
                velocity.y = player.currentStats.GroundingForce;
            }
            else
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
        }

        #endregion
        #region Player Checks


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