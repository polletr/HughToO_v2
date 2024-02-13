using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;
using System;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;

public class AttackState : PlayerState
{
    Collider2D hitBox;
    public override void EnterState()
    {
        //PlayAnimation
    }
    public override void OnAttack()
    {
        hitBox = player.AttackHitBox;
        hitBox.enabled = true;
        Debug.Log("Attack");
        //call change state to idle after animation
        //TaskAwaiter().Delay(2f).GetAwaiter().OnCompleted(LeaveAttack);

         //async ()=>player.ChangeState(new IdleState()),2f;// SetTimeout();
    }



    void LeaveAttack()
    {
        player.ChangeState(new IdleState());

    }
    public override void StateFixedUpdate()
    {

        // base.StateFixedUpdate();//dont call base
    }
    public override void ExitState() 
    {
      hitBox.enabled = false;
    }
}
