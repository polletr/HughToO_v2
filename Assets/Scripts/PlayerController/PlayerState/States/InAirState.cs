using UnityEngine;

public class InAirState : JumpState
{
    public override void EnterState()
    {

    }
    public override void StateUpdate()
    {

    }
    public override void ExitState()
    {
    }

    public override void StateFixedUpdate()
    {
        CanUseCoyote = _coyoteUsable && !player.GroundCheck() && _time < _frameLeftGrounded + player.currentStats.CoyoteTime;
        HandleGravity();
        if (player._rb.velocity.y < 0)
        {
            //play animation of falling
        }

        if (inputManager.Movement.x == 0)
        {
            var deceleration = player.currentStats.AirDeceleration;
            velocity.x = Mathf.MoveTowards(player._rb.velocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            velocity.x = inputManager.Movement.x * player.currentStats.MaxSpeed;
        }

        if ((inputManager.Movement.x > 0f && player.transform.localScale.x < 0) || (inputManager.Movement.x < 0f && player.transform.localScale.x > 0))
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x *= -1f;
            player.transform.localScale = localScale;
        }

        base.StateFixedUpdate();
    }


    protected void HandleGravity()
    {
        if (player.GroundCheck())
        {
            //Play landing sound
            //Player landing animation
            if (inputManager.Movement.x != 0)
            {
                player.ChangeState(new MoveState());
            }
            else
            {
                player.ChangeState(new IdleState());
            }

        }
        else
        {

            var inAirGravity = player.currentStats.FallAcceleration;
            if (_endedJumpEarly && player._rb.velocity.y > 0) 
                inAirGravity *= player.currentStats.JumpEndEarlyGravityModifier;

            velocity.y = Mathf.MoveTowards(player._rb.velocity.y, -player.currentStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);


        }

    }

}
