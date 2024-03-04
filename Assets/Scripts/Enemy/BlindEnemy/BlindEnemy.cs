using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindEnemy : EnemyBase
{
    private enum State
    {
        Idle,
        Patrolling,
        Stunned,
        Attacking
    }

    private State currentState; //this keeps track of the current state


    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;

    private Transform currentPoint;
    private Transform lastPoint;

    [SerializeField]
    private float moveSpeed;

    private float vx;

    private bool facingLeft;

    [SerializeField]
    private float waitTime;

    public override void Start()
    {
        base.Start();
        lastPoint = pointA;

        

        if (this.gameObject != null)
        {
            SetState(State.Idle);
        }
    }

    public override void Update()
    {
        base.Update();
        if (currentState != State.Stunned && isStunned)
        {
            SetState(State.Stunned);
        }
    }
    private void Flip()
    {
        // get the current scale
        Vector2 localScale = transform.localScale;
        vx = rb.velocity.x;
        if (vx < 0) // moving right so face right
        {
            facingLeft = false;
        }
        else if (vx > 0)
        { // moving left so face left
            facingLeft = true;
        }

        // check to see if scale x is right for the player
        // if not, multiple by -1 which is an easy way to flip a sprite
        if (((facingLeft) && (localScale.x < 0)) || ((!facingLeft) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        // update the scale
        transform.localScale = localScale;
    }

    private void SetState(State newState)
    {
        //what we want to do here is look at the newstater, compare it to the enumvalues, and then figure out what to do based on that.
        //set state will only be called when a state changes
        currentState = newState;
        StopAllCoroutines();//stop the previous coroutines so they aren't operating at the same time

        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(OnIdle());
                //do some work
                break;
            case State.Patrolling:
                StartCoroutine(OnPatrolling());
                //do some work
                break;
            case State.Attacking:
                StartCoroutine(OnAttacking());
                //do some work
                break;

            case State.Stunned:
                StartCoroutine(OnStunned());
                //do some work
                break;
            default:
                break;
        }
        ///
    }

    private IEnumerator OnIdle() //handles our idle state
    {
        anim.SetBool("walking", false);

        rb.velocity = new Vector2(0f, 0f);

        while ((currentPoint == null))
        {
            if (lastPoint == pointA)
            {
                currentPoint = pointB;
            }
            else if (lastPoint == pointB)
            {
                currentPoint = pointA;
            }

            yield return new WaitForSecondsRealtime(waitTime);
        }


        SetState(State.Patrolling); //we found a point, we now need to move

    }

    private IEnumerator OnPatrolling()
    {
        anim.SetBool("walking", true);

        while (currentPoint != null && isAlive && !isStunned)
        {
            Vector3 direction = (currentPoint.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x, 0f) * moveSpeed;

            Flip();

            if (Vector3.Distance(transform.position, currentPoint.position) <= 0.2f)
            {
                lastPoint = currentPoint;
                currentPoint = null;
            }

            yield return null;
        }

        SetState(State.Idle);

    }

    private IEnumerator OnStunned()
    {
        anim.SetBool("walking", false);

        while (isAlive && isStunned)
        {
            Debug.Log("Stunned");
            if (!isStunned)
                SetState(State.Idle);
            yield return null;

        }

    }

    private IEnumerator OnAttacking()
    {
        anim.SetTrigger("attack");
        AudioManager.Instance.PlaySFX(AudioManager.Instance._audioClip.EnemyAttack);
        yield return new WaitForSeconds(1f);
        SetState(State.Idle);
    }

    public void Attacked()
    {
        rb.velocity = new Vector2(0f, 0f);

        SetState(State.Attacking);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.1f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.1f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

        // Draw a box to represent the field of vision
        Gizmos.color = Color.red; // You can choose any color you like
    }

    public override void Die()
    {
        base.Die();
        StopAllCoroutines();

    }


}
