using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class JumpState : PlayerState
{
    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private FrameInput _frameInput;
    public Vector2 FrameInput => _frameInput.Move;

    float _time;
    private float _frameLeftGrounded = float.MinValue;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + player.currentStats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !player.GroundCheck() && _time < _frameLeftGrounded + player.currentStats.CoyoteTime;

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
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        velocity.y = player.currentStats.JumpPower;
    }

    public override void StateUpdate()
    {
        _time += Time.deltaTime;
        GatherInput();

    }
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        HandleJump();

        ApplyMovement();
        //player.ChangeState(new InAirState());

    }

    public override void HandleGravity()
    {
        if (player.GroundCheck() && velocity.y <= 0f)
        {
            //Play landing sound
            //Player landing animation
            player.ChangeState(new IdleState());
        }
        else
        {
            var inAirGravity = player.currentStats.FallAcceleration;
            if (_endedJumpEarly && velocity.y > 0) inAirGravity *= player.currentStats.JumpEndEarlyGravityModifier;
            velocity.y = Mathf.MoveTowards(velocity.y, -player.currentStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            if (velocity.y < 0)
            {
                //play animation of falling
            }
            else if (velocity.y > 0)
            {
                //play animation of jumping
            }

        }

    }

    private void ApplyMovement() => player.Velocity = velocity;





}
