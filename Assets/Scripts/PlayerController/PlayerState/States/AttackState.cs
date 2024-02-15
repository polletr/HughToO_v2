using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;

public class AttackState : PlayerState
{
    GameObject hitBox;
    float timer;
    public override void EnterState()
    {
        //PlayAnimation
        timer = 0f;
    }
    public override void OnAttack()
    {
        hitBox = player.AttackHitBox;
        hitBox.SetActive(true);
        //call change state to idle after animation
        //TaskAwaiter().Delay(2f).GetAwaiter().OnCompleted(LeaveAttack);

        //async ()=>player.ChangeState(new IdleState()),2f;// SetTimeout();
    }

    public override void StateFixedUpdate()
    {
        var deceleration = player.currentStats.GroundDeceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
        player.Velocity = velocity;

        // base.StateFixedUpdate();//dont call base
        timer += Time.deltaTime;

        Debug.Log(timer);
        if (timer >=2f)
        {
            player.ChangeState(new IdleState());
        }
    }
    public override void ExitState() 
    {
        hitBox.SetActive(false);
    }
}
