using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private enum State
    {
        Idle,
        Moving
    }

    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float WaitTimeInIdle;

    private Transform currentPoint;
    private Transform lastPoint;

    protected Rigidbody2D rb;

    private State currentState; //this keeps track of the current state

    private void SetState(State newState)
    {
        //set state will only be called when a state changes
        currentState = newState;
        StopAllCoroutines();//stop the previous coroutines so they aren't operating at the same time

        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(OnIdle());
                //do some work
                break;
            case State.Moving:
                StartCoroutine(OnMoving());
                //do some work
                break;
            default:
                break;
        }
        ///
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPoint = pointA;
        SetState(State.Idle);
    }

    private IEnumerator OnIdle() //handles our idle state
    {

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

            yield return new WaitForSecondsRealtime(WaitTimeInIdle);
        }

        SetState(State.Moving); //we found a point, we now need to move

    }

    // Update is called once per frame
    private IEnumerator OnMoving()
    {
        bool moveRight = true;

        while (currentPoint != null)
        {
            Vector2 direction = (currentPoint.position - transform.position).normalized;
            //rb.velocity = new Vector2(direction.x, 0f) * moveSpeed;

            if (transform.position.x < currentPoint.position.x)
            {
                moveRight = true;
            }
            else
            { 
                moveRight = false;
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }


            if (Vector2.Distance(transform.position, currentPoint.position) <= 0.05f)
            {
                lastPoint = currentPoint;
                currentPoint = null;
            }

            yield return null;
        }

        SetState(State.Idle);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

        // Draw a box to represent the field of vision
        Gizmos.color = Color.red; // You can choose any color you like
    }
}
