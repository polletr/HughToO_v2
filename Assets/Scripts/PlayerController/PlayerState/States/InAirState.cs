using HughTo0;
using UnityEngine;

public class InAirState : PlayerState
{
    //change this later
    [SerializeField]
    float jumpReleasedAcceleration = 1.5f;
    public override void EnterState()
    {
        base.EnterState();
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
        exitFromInAir = true;

    }

    public override void StateFixedUpdate()
    {
        //CanUseCoyote = _coyoteUsable && !player.GroundCheck() && _time < _frameLeftGrounded + player.currentStats.CoyoteTime;
        HandleGravity();

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

            var inAirGravity = player.currentStats.FallAcceleration * (inputManager.IsJumpHeldDown ? 1.0f : jumpReleasedAcceleration);
            if (_endedJumpEarly && player._rb.velocity.y > 0)
                inAirGravity *= player.currentStats.JumpEndEarlyGravityModifier;

            velocity.y = Mathf.MoveTowards(velocity.y, -player.currentStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            //Debug.Log(velocity.y);

        }

    }

}
