using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindEnemy : EnemyBase
{
    private enum State
    {
        Idle,
        Patrolling
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

    private Transform target;
    private Vector2 moveDirection;

    private float vx;

    private bool facingLeft;

    public override void Start()
    {
        base.Start();
        lastPoint = pointA;

        if (this.gameObject != null)
        {
            SetState(State.Idle);
        }
    }
    private void Flip()
    {
        // get the current scale
        Vector2 localScale = transform.localScale;
        vx = rb.velocity.x;
        if (vx < 0) // moving right so face right
        {
            facingLeft = true;
        }
        else if (vx > 0)
        { // moving left so face left
            facingLeft = false;
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
            default:
                break;
        }
        ///
    }

    private IEnumerator OnIdle() //handles our idle state
    {
        //animator.SetBool("Walking", false);

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

            yield return new WaitForSeconds(2f);
        }


        SetState(State.Patrolling); //we found a point, we now need to move

    }

    private IEnumerator OnPatrolling()
    {
       // animator.SetBool("Walking", true);

        while (currentPoint != null && isAlive)
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

        //After Value turns 1, he is going to search for a new spot
        SetState(State.Idle);

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
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Collider2D>().enabled = false;

    }


}
