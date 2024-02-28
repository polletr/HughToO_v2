using HughTo0;
using UnityEngine;

public class GroundState : PlayerState
{

    public override void EnterState()
    {
        base.EnterState();
        velocity.y = player.currentStats.GroundingForce;

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();



    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}