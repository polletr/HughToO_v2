using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackState : GroundState
{
    private float KBCount;

    public override void EnterState()
    {
        KBCount = player.currentStats.KBCounter;
        player.anim.SetTrigger("getHit");

        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();

        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public override void StateFixedUpdate()
    {
        if (player.currentStats.knockFromRight)
        {
            velocity = new Vector2 (-player.currentStats.KBForce, player.currentStats.KBForce);
        }
        else
        {
            velocity = new Vector2(player.currentStats.KBForce, player.currentStats.KBForce);
        }


        KBCount -= Time.fixedDeltaTime;

        if (KBCount < 0)
        {
            var inAirGravity = player.currentStats.KBFallAcceleration;
            velocity.y = Mathf.MoveTowards(player._rb.velocity.y, -player.currentStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            if (player.GroundCheck())
            {
                player.ChangeState(new IdleState());
            }
        }

        player._rb.velocity = velocity;

    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }


}
