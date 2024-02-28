using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;
using Unity.VisualScripting;

public class DashState : PlayerState
{
   //private Vector2 velocity;
   public override void  EnterState()
   {

   }
    public override void ExitState()
    {
         
    }
    public override void StateFixedUpdate()
    {

        if(inputManager.Movement.x > 0 )
        {
            velocity.x +=  player.currentStats.MaxSpeed + player.currentStats.DashSpeed;
            player._rb.velocity = velocity;
            player._rb.velocity = new Vector2(0, player._rb.velocity.y);
            player.ChangeState(new MoveState());
        }
        else if(inputManager.Movement.x < 0)
        {
            velocity.x += -player.currentStats.MaxSpeed - player.currentStats.DashSpeed;
            player._rb.velocity = velocity;
            player._rb.velocity = new Vector2(0, player._rb.velocity.y);
            player.ChangeState(new MoveState());
        }
        else
        {
            player.ChangeState(new IdleState());
        }



        if (velocity.x == 0)
        {
            player.ChangeState(new IdleState());
        }

        if (player.GroundCheck() && inputManager.IsJumpHeldDown)
        {
            player.ChangeState(new JumpState());
        }

        base.StateFixedUpdate();

    }


}
