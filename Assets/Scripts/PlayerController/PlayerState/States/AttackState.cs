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
        PlayAttckSound();
        timer = 0f;
    }
    public void PlayAttckSound()
    {
        switch(player.currentStats.currentForm)        {
            case ScriptableStats.Form.Gas:
                AudioManager.Instance.PlayPlayerSFX(AudioManager.Instance._audioClip.WindStun);
                break;
            case ScriptableStats.Form.Water:
                AudioManager.Instance.PlayPlayerSFX(AudioManager.Instance._audioClip.WaterSplash);
                break;
            case ScriptableStats.Form.Ice:
                AudioManager.Instance.PlayPlayerSFX(AudioManager.Instance._audioClip.IcePunch);
                break;
        }
    }
    public override void StateFixedUpdate()
    {
        var deceleration = player.currentStats.GroundDeceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
        player._rb.velocity = velocity;

        // base.StateFixedUpdate();//dont call base
        timer += Time.deltaTime;
        float clipLength = player.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        if (timer >= clipLength + 0.5f)
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
