using UnityEngine;

namespace HughTo0
{
    public abstract class PlayerState
    {
        public Player player { get; set;}

        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void StateFixedUpdate() { }
        public virtual void StateUpdate() { }

        #region Player Actions 

        public virtual void OnMovement(Vector2 movement) { }
        public virtual void OnJump() { }
        public virtual void OnGlid() { }
        public virtual void OnAttack() { }
        public virtual void OnDash() { }
        public virtual void OnFalling() { }


        public virtual void OnWaterForm() { }
        public virtual void OnIceForm() { }
        public virtual void OnWindForm() { }


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