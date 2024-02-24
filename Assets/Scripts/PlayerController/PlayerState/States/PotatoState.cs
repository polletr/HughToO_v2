using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class PotatoState : PlayerState
{
    float _potatoTimer = 0;
    public float _potatoTime;
    public override void EnterState()
    {
        _potatoTimer = 0;
    }
    public override void ExitState()
    {
       
    }
    public override void StateUpdate()
    {
        
        _potatoTimer += Time.deltaTime;
        if (_potatoTimer >= _potatoTime)
        {
            player.ChangeState(new IdleState());  
        }
    }

    public override void StateFixedUpdate()
    {
        var inAirGravity = player.currentStats.FallAcceleration/5f;

        float yVelocity = Mathf.MoveTowards(player._rb.velocity.y, -player.currentStats.MaxFallSpeed/2f, inAirGravity * Time.fixedDeltaTime);

        player._rb.velocity = new Vector2(0, yVelocity);
    }

    public void ChangeTime(float time)
    {
        _potatoTime = time;
    }

}
