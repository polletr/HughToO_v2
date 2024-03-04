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
        player.anim.SetTrigger("jumping");
        AudioManager.Instance.PlayPlayerSFX(AudioManager.Instance._audioClip.Jump);
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
