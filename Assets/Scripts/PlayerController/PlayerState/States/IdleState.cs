using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class IdleState : PlayerState
{
    public override void EnterState() 
    { 
    //PlayAnimation
    }

    public override void ExitState() { }
    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (player.GroundCheck() && inputManager.IsJumpHeldDown)
        {
            player.ChangeState(new JumpState());
        }

        if (inputManager.Movement.x != 0 )
        {
            player.ChangeState(new MoveState());
        }

    }






}