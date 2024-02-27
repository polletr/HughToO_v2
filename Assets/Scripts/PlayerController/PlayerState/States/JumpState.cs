using HughTo0;
using UnityEngine;

public class JumpState : PlayerState
{

    protected float _time;
    protected float _frameLeftGrounded = float.MinValue;

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = Time.time;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        velocity.y = player.currentStats.JumpPower;
        Debug.LogFormat("Current velocity when executing a jump is {0}", velocity.y);
        player._rb.velocity = new Vector2(player._rb.velocity.x, velocity.y);
        player.ChangeState(new InAirState());
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.LogFormat("Enter Jump State with a velocity of {0}", player._rb.velocity.y);
        player.anim.SetTrigger("jumping");
        ExecuteJump();
    }

    public override void ExitState()
    {
      base.ExitState();
    }

    public override void StateFixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        

        base.StateFixedUpdate();


    }






}
