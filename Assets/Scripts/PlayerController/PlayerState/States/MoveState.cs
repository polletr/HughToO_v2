using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class MoveState : GroundState
{
    public override void EnterState() 
    { 
    //PlayAnimation
    }

    public override void ExitState() { } 

    public override void StateFixedUpdate()
    {

        if (inputManager.Movement.x == 0 && !exitFromInAir)
        {
            var deceleration = player.currentStats.GroundDeceleration ;
            velocity.x = Mathf.MoveTowards(player._rb.velocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            if (exitFromInAir)
            {
                velocity.x = inputManager.Movement.x * player.currentStats.MaxSpeed;
                exitFromInAir = false;
                Debug.Log("exitFromInAir");
            }
            else
            {
                velocity.x = Mathf.MoveTowards(player._rb.velocity.x, inputManager.Movement.x * player.currentStats.MaxSpeed, player.currentStats.Acceleration * Time.fixedDeltaTime);
            }
        }

        if ((inputManager.Movement.x > 0f && player.transform.localScale.x < 0) || (inputManager.Movement.x < 0f && player.transform.localScale.x > 0))
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x *= -1f;
            player.transform.localScale = localScale;
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