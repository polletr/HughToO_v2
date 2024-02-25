using UnityEngine;

public class AttackState : GroundState
{
    GameObject hitBox;
    float timer;
    public override void EnterState()
    {
        player.canDash = false;
        player.anim.SetTrigger("attacking");
        hitBox = player.AttackHitBox;
        hitBox.SetActive(true);

        timer = 0f;
    }

    public override void StateFixedUpdate()
    {
        var deceleration = player.currentStats.GroundDeceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
        player._rb.velocity = velocity;

        // base.StateFixedUpdate();//dont call base
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            player.ChangeState(new IdleState());
        }
    }
    public override void ExitState()
    {
        if(player.currentStats.currentForm == ScriptableStats.Form.Gas)
            player.canDash = true;
        hitBox.SetActive(false);
    }
}
