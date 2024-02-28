using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class MoveState : GroundState
{
    public override void EnterState() 
    {
        base.EnterState();

        Debug.Log("Enter Move State");
        player.anim.SetBool("moving", true);
        if (HasBufferedJump)
        {
            HandleJump();
        }

    }

    public override void ExitState() 
    {
        player.anim.SetBool("moving", false);
    }


    public override void HandleJump()
    {
        if (!_endedJumpEarly && !player.GroundCheck() && !inputManager.IsJumpHeldDown && velocity.y > 0)
            _endedJumpEarly = true;

        player.ChangeState(new JumpState());

    }

    public override void StateFixedUpdate()
    {

        if (inputManager.Movement.x == 0)
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
        else if (!player.GroundCheck())
        {
            player.ChangeState(new InAirState());
        }


        base.StateFixedUpdate();
    }




}