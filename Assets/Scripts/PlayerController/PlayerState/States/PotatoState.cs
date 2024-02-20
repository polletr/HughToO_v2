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
        player._rb.velocity = Vector2.zero;
    }

    public void ChangeTime(float time)
    {
        _potatoTime = time;
    }

}
