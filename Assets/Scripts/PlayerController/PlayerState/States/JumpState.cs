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
        HandleJump();
        OnMovement(inputManager.Movement);
        ApplyMovement();

        //base.StateFixedUpdate();

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

    public override void OnMovement(Vector2 movement)
    {
        if (movement.x == 0)
        {
            var deceleration = player.GroundCheck() ? player.currentStats.GroundDeceleration : player.currentStats.AirDeceleration;
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, movement.x * player.currentStats.MaxSpeed, player.currentStats.Acceleration * Time.fixedDeltaTime);
        }

        if ((movement.x > 0f && player.transform.localScale.x < 0) || (movement.x < 0f && player.transform.localScale.x > 0))
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x *= -1f;
            player.transform.localScale = localScale;
        }

        HandleGravity();

    }


    private void ApplyMovement() => player.Velocity = velocity;





}
