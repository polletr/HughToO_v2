using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowingTree : MonoBehaviour
{
    [SerializeField]
    public enum growthDirection
    {
        Down,
        Up
    }

    private enum State
    {
        Growing,
        Retracting,
        Idle
    }

    private State currentState;

    public growthDirection currentDirection;
    private Vector2 finalDirection;

    [SerializeField]
    private float originalSpeed;

    private float speed = 0f;

    [SerializeField]
    private Transform desiredPos;

    private Transform currentPos;

    private Vector2 startPos;

    bool playerOnCollider;

    [SerializeField]
    private bool fixedTree;

    private Animator anim;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
        currentPos = transform;
        startPos = currentPos.position;
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        GetVector(currentDirection);
        SetState(State.Idle);
    }

    private void GetVector(growthDirection direction)
    {
        switch (direction)
        {
            case growthDirection.Up:
                finalDirection = Vector2.up;
                break;
            case growthDirection.Down:
                finalDirection = Vector2.down;
                break;
            default:
                break;
        }
    }

    private void SetState(State state)
    {
        switch (state)
        {
            case State.Idle:
                currentState = State.Idle;
                break;
            case State.Growing:
                currentState = State.Growing;
                StopAllCoroutines();
                StartCoroutine(GrowTree());
                break;
            case State.Retracting:
                currentState = State.Retracting;
                StopAllCoroutines();
                StartCoroutine(RetractTree());
            break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(playerOnCollider);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && currentState != State.Growing /*&& Add Check for water form*/)
        {
            SetState(State.Growing);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!fixedTree && other.gameObject.tag == "Player" && currentState != State.Retracting)
        {
            SetState(State.Retracting);
        }

    }

    IEnumerator GrowTree()
    {
        while (Mathf.Abs(Vector2.Distance(currentPos.position, desiredPos.position)) > 0.05f)
        {
            transform.Translate(finalDirection * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            currentPos.position = transform.position;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            yield return null;
        }

        SetState(State.Idle);
    }

    IEnumerator RetractTree()
    {
        while (Mathf.Abs(Vector2.Distance(currentPos.position, startPos)) > 0.01f)
        {
            transform.Translate(finalDirection * -1 * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            currentPos.position = transform.position;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            yield return null;
        }
    }




    private void OnDecrease()
    {
        if (transform.position != currentPos.position)
        {
            speed = -originalSpeed;
        }
    }

}
