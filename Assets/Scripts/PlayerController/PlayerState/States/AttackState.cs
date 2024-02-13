using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughTo0;

public class AttackState : PlayerState
{
    Collider2D hitBox;
    public override void OnAttack()
    {
        hitBox = player.AttackHitBox;
        hitBox.enabled = true;
       // Debug.Log("Attack");
        player.ChangeState(new GroundState());
    }
    public override void ExitState() 
    {
      hitBox.enabled = false;
    }
}
