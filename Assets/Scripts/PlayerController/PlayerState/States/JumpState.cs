using HughTo0;
using UnityEngine;

public class JumpState : GroundState
{
    protected bool _jumpToConsume;
    protected bool _bufferedJumpUsable;
    protected bool _endedJumpEarly;
    protected bool _coyoteUsable;
    protected float _timeJumpWasPressed;

    protected bool CanUseCoyote;

    protected float _time;
    protected float _frameLeftGrounded = float.MinValue;
    private bool HasBufferedJump => Time.time - inputManager.JumpButtonPressedLast < player.currentStats.JumpBuffer;//_bufferedJumpUsable && _time < _timeJumpWasPressed + player.currentStats.JumpBuffer;
    


    private void GatherInput()
    {
        if (inputManager.IsJumpHeldDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }

    }

    private void HandleJumpp()
    {
        if (!_endedJumpEarly && !player.GroundCheck() && !inputManager.IsJumpHeldDown && velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume || !HasBufferedJump) return;

        if (player.GroundCheck() || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = Time.time;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        velocity.y = player.currentStats.JumpPower;
        player._rb.velocity = new Vector2(player._rb.velocity.x, player._rb.velocity.y + velocity.y);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

    }
    public override void EnterState()
    {
        base.EnterState();
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
        
        GatherInput();
        HandleJumpp();


        base.StateFixedUpdate();

        //player.ChangeState(new InAirState());

    }


    /*public override void OnMovement(Vector2 movement)
    {
        base.OnMovement(movement);

        // HandleGravity();

    }*/





}
