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
    private Player player;
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
            Vector2 velocity = new Vector2();
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
                velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, 0);
            }
            else
            {
                velocity = new Vector2(-moveSpeed * Time.fixedDeltaTime, 0);
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
            if (player)
            {
                player.ParentVelocity = velocity;
                Debug.LogFormat("Set parent velocity to {0}", velocity.x);
            }
                
            

            if (Vector2.Distance(transform.position, currentPoint.position) <= 0.05f)
            {
                lastPoint = currentPoint;
                currentPoint = null;
            }

            yield return new WaitForFixedUpdate();
        }

        SetState(State.Idle);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            player = collision.gameObject.GetComponent<Player>();

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
            if(player != null)
                player.ParentVelocity = new Vector2();
            player = null;// collision.gameObject.GetComponent<Player>();
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
