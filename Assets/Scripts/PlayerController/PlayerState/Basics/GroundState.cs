using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;
using Percy;

public class GroundState : PlayerState
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

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + player.stats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !GroundCheck() && _time < _frameLeftGrounded + player.stats.CoyoteTime;

    private void GatherInput()
    {
        _frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
        };

        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }

    }

    private void HandleJump()
    {
        if (!_endedJumpEarly && !GroundCheck() && !_frameInput.JumpHeld && velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (GroundCheck() || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        velocity.y = player.stats.JumpPower;
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

    public override void OnJump()
    {

    }
   

    public override void StateFixedUpdate()
    {
        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();
        //player.ChangeState(new InAirState());
    }


    private void HandleGravity()
    {
        if (GroundCheck() && velocity.y <= 0f)
        {
            velocity.y = player.stats.GroundingForce;
        }
        else
        {
            var inAirGravity = player.stats.FallAcceleration;
            if (_endedJumpEarly && velocity.y > 0) inAirGravity *= player.stats.JumpEndEarlyGravityModifier;
            velocity.y = Mathf.MoveTowards(velocity.y, -player.stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    private void ApplyMovement() => player.Velocity = velocity;





}
