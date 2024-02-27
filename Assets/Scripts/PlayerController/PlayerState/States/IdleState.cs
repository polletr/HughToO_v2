using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class IdleState : GroundState
{
    public override void EnterState() 
    { 
        base.EnterState();
        if (HasBufferedJump)
        {
            HandleJump();
        }
    //PlayAnimation
    }

    public override void ExitState() { }

    public override void HandleJump()
    {
        if (!_endedJumpEarly && !player.GroundCheck() && !inputManager.IsJumpHeldDown && velocity.y > 0)
            _endedJumpEarly = true;

            player.ChangeState(new JumpState());

    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        //if (player.GroundCheck() && inputManager.IsJumpHeldDown)
        //{
        //    player.ChangeState(new JumpState());
        //}

        if (inputManager.Movement.x != 0 )
        {
            player.ChangeState(new MoveState());
        }
        else if (!player.GroundCheck())
        {
            player.ChangeState(new InAirState());
        }


    }


}