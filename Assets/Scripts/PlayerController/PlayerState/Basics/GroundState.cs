using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;
using Percy;

public class GroundState : PlayerState
{

    bool isJumping = false;

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }




    public override void OnMovement(Vector2 movement)
    {
        Debug.Log("GroundState: OnMovement");
    }

    public override void OnJump()
    {
        Debug.Log("GroundState: OnJump");
        isJumping = true;

    }

    public override void StateFixedUpdate()
    {
        Debug.Log("GroundState: StateUpdate");
        //jump
        if (isJumping)
        {
            isJumping = false;
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            //change after actual jump action
            player.ChangeState(new InAirState());
        }


    }


}
