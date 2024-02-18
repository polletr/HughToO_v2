using HughTo0;
using UnityEngine;

public class GroundState : PlayerState
{
    protected Vector2 velocity;
    protected bool exitFromInAir;
    public override void EnterState()
    {
        base.EnterState();
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
        if (player.GroundCheck() && velocity.y <= 0)
        {
            velocity.y = player.currentStats.GroundingForce;//fix
        }
        else if (player.currentState is not InAirState)// fix  
        {
            player.ChangeState(new InAirState());
        }


        player._rb.velocity = velocity;

    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}