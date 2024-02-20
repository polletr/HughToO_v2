using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class PotatoState : PlayerState
{
    float _potatoTimer = 0;
    float _potatoTime;
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
        
    }

    public void EnterPotatoState(float time)
    {
        player.ChangeState(new PotatoState());
        _potatoTime = time;
    }

}
