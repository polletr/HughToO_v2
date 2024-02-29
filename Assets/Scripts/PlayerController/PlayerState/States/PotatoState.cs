using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class PotatoState : PlayerState
{
    float _potatoTimer;
    public float _potatoTime;
    public override void EnterState()
    {
        Debug.Log("Enter Potato");
         inputManager.DisableForms();
        _potatoTimer = 0f;

        player._rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
        player.anim.SetBool("isAlive", false);

        float clipLength = player.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Debug.Log(player.anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        if (clipLength > 0.5f)
        {
            _potatoTime = clipLength * 3f;
        }
        else
        {
            _potatoTime = 2f;
        }
    }
    public override void ExitState()
    {
        player.anim.SetBool("isAlive", true);
        player._rb.constraints = RigidbodyConstraints2D.None;
        player._rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        inputManager.EnableForms();
    }
    public override void StateUpdate()
    {

        _potatoTimer += Time.deltaTime;
        if (_potatoTimer >= _potatoTime)
        {
            Debug.Log(_potatoTimer);

            player.ChangeState(new IdleState());
        }
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        var inAirGravity = player.currentStats.FallAcceleration / 5f;

        float yVelocity = Mathf.MoveTowards(player._rb.velocity.y, -player.currentStats.MaxFallSpeed / 2f, inAirGravity * Time.fixedDeltaTime);
        velocity.y = yVelocity;
        velocity.x = 0f;
    }

}
