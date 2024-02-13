using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class MoveState : PlayerState
{
    public override void EnterState() 
    { 
    //PlayAnimation
    }

    public override void ExitState() { }
    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (velocity.x == 0)
        { 
            player.ChangeState(new IdleState());

        }
    }




}