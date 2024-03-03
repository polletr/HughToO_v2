using UnityEngine;

public class GlideState : InAirState
{

    public override void EnterState()
    {
        Debug.Log("Enter Glide");
        player.anim.SetBool("gliding", true);
    }
    public override void StateUpdate()
    {
        if (!inputManager.IsGliding)
        {
            player.ChangeState(new InAirState());
        }
    }
    public override void ExitState()
    {
        player.anim.SetBool("gliding", false);
    }
    protected override void HandleGravity()
    {
        if (player.GroundCheck())
        {
            //Play landing sound
            //Player landing animation

            if (inputManager.Movement.x != 0)
                player.ChangeState(new MoveState());
            else
                player.ChangeState(new IdleState());

        }
        else 
        {
            var inAirGravity = player.currentStats.FallAcceleration;
/*            if (_endedJumpEarly && player._rb.velocity.y > 0)
                inAirGravity *= player.currentStats.JumpEndEarlyGravityModifier;
*/
            velocity.y = Mathf.MoveTowards(player._rb.velocity.y, -player.currentStats.GlideFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }

    }


}
