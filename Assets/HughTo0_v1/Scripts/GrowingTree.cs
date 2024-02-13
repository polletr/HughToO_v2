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

    public growthDirection currentDirection;
    private Vector2 finalDirection;

    [SerializeField]
    private float originalSpeed;

    private float speed = 0f;

    [SerializeField]
    private Transform desiredPos;

    private Transform currentPos;

    private Vector2 startPos;


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

    private void SetState(State currentState)
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Growing:
                StopAllCoroutines();
                StartCoroutine(GrowTree());
                break;
            case State.Retracting:
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

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" /*&& Add Check for water form*/)
        {
            SetState(State.Growing);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!fixedTree && other.gameObject.tag == "Player")
        {
            SetState(State.Retracting);
        }

    }

    IEnumerator GrowTree()
    {
        while (Mathf.Abs(Vector2.Distance(currentPos.position, desiredPos.position)) > 0.01)
        {
            transform.Translate(finalDirection * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            currentPos.position = transform.position;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        yield return null;
    }

    IEnumerator RetractTree()
    {
        while (Mathf.Abs(Vector2.Distance(currentPos.position, startPos)) > 0.01)
        {
            transform.Translate(finalDirection * -1 * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            currentPos.position = transform.position;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        yield return null;
    }




    private void OnDecrease()
    {
        if (transform.position != currentPos.position)
        {
            speed = -originalSpeed;
        }
    }

}
