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

    [SerializeField]
    private GameObject TopVines;

    [SerializeField]
    private Sprite HealthyVines;

    [SerializeField]
    private Sprite DeadVines;


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


    public void OnInteracted()
    {
        if (currentState == State.Idle)
        {
            TopVines.GetComponent<SpriteRenderer>().sprite = HealthyVines;
            SetState(State.Growing);

        }
    }

    IEnumerator GrowTree()
    {
        anim.SetTrigger("Shake");

        while (Mathf.Abs(Vector2.Distance(currentPos.position, desiredPos.position)) > 0.1f)
        {
            transform.Translate(finalDirection * speed * Time.deltaTime);
            currentPos.position = transform.position;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            yield return null;
        }
        Invoke("CallRetract", 4f);
    }

    private void CallRetract()
    {
        if (!fixedTree)
        {
            SetState(State.Retracting);
        }

    }

    IEnumerator RetractTree()
    {
        TopVines.GetComponent<SpriteRenderer>().sprite = DeadVines;
        anim.SetTrigger("Shake");


        while (Mathf.Abs(Vector2.Distance(currentPos.position, startPos)) > 0.1f)
        {
            transform.Translate(finalDirection * -1 * speed * Time.deltaTime);
            currentPos.position = transform.position;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            yield return null;
        }
        SetState(State.Idle);
    }


}
