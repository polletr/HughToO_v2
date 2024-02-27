using HughTo0;
using UnityEngine;

public class InAirState : PlayerState
{
    public override void EnterState()
    {
        Debug.Log("Enter InAir State");
        player.anim.SetBool("falling", true);
    }
    public override void StateUpdate()
    {
        if (inputManager.IsGliding && player.currentStats.currentForm == ScriptableStats.Form.Gas && player._rb.velocity.y < 0)
        {
            player.ChangeState(new GlideState());
        }

    }
    public override void ExitState()
    {
        player.anim.SetBool("falling", false);
    }

    public override void StateFixedUpdate()
    {
        //CanUseCoyote = _coyoteUsable && !player.GroundCheck() && _time < _frameLeftGrounded + player.currentStats.CoyoteTime;
        HandleGravity();
        Debug.Log("Fixed Update In Air");

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


    protected virtual void HandleGravity()
    {
/*        if (player.GroundCheck())
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
*/      if (true)
        {

            var inAirGravity = player.currentStats.FallAcceleration;
            if (_endedJumpEarly && player._rb.velocity.y > 0)
                inAirGravity *= player.currentStats.JumpEndEarlyGravityModifier;

            velocity.y = Mathf.MoveTowards(velocity.y, -player.currentStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            Debug.Log(velocity.y);

        }

    }

}
