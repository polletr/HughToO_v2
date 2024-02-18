using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;

public class AttackState : GroundState
{
    GameObject hitBox;
    float timer;
    public override void EnterState()
    {
        //PlayAnimation
        hitBox = player.AttackHitBox;
        hitBox.SetActive(true);

        timer = 0f;
    }

    public override void StateFixedUpdate()
    {
        var deceleration = player.currentStats.GroundDeceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
        player._rb.velocity = velocity;

        // base.StateFixedUpdate();//dont call base
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            player.ChangeState(new IdleState());
        }
    }
    public override void ExitState() 
    {
        hitBox.SetActive(false);
    }
}
