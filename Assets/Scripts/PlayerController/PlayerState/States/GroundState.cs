using HughTo0;
using UnityEngine;

public class GroundState : PlayerState
{
    protected bool exitFromInAir;

    public override void EnterState()
    {
        base.EnterState();
        velocity.y = player.currentStats.GroundingForce;//fix

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void StateFixedUpdate()
    {
        if (player.currentState is InAirState)
        {
            exitFromInAir = true;
        }
        base.StateFixedUpdate();



    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}