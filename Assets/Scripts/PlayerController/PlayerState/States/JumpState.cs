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
    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + player.currentStats.JumpBuffer;


    private void GatherInput()
    {
        if (inputManager.IsJumpHeldDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }

    }

    private void HandleJump()
    {
        if (!_endedJumpEarly && !player.GroundCheck() && !inputManager.IsJumpHeldDown && velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (player.GroundCheck() || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        Debug.Log("Jumping");
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        velocity.y = player.currentStats.JumpPower;
        player._rb.velocity = new Vector2(player._rb.velocity.x, player._rb.velocity.y + velocity.y);
        Debug.Log(player._rb.velocity.y);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        _time += Time.deltaTime;
        GatherInput();
        HandleJump();

    }
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
       

        base.StateFixedUpdate();

        //player.ChangeState(new InAirState());

    }


    /*public override void OnMovement(Vector2 movement)
    {
        base.OnMovement(movement);

        // HandleGravity();

    }*/





}
