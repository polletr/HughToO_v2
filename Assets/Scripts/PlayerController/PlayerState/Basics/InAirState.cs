using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;
using Percy;

public class InAirState : PlayerState
{
    bool isGrounded = false;
    bool _endedJumpEarly = false;

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        velocity.y = player.stats.GroundingForce;
        _endedJumpEarly = false;
    }


    public override void StateFixedUpdate()
    {
        HandleDirection();
        if(isJumping) velocity.y = player.stats.JumpPower;

        if (GroundCheck() && velocity.y <= 0f)
        {
            player.ChangeState(new GroundState());
        }
        else
        {
            var inAirGravity = player.stats.FallAcceleration;
            if (velocity.y > 0 && _endedJumpEarly) inAirGravity *= player.stats.JumpEndEarlyGravityModifier;
            velocity.y = Mathf.MoveTowards(velocity.y, -player.stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }

        player.Velocity = velocity;

    }

}                
