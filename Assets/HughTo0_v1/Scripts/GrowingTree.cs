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
                GetComponent<BoxCollider2D>().isTrigger = false;

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

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (currentState != State.Retracting && other.gameObject.GetComponent<Player>()?.currentStats.currentForm != ScriptableStats.Form.Water)
        {
            SetState(State.Retracting);
        }
        else if (currentState != State.Growing && other.gameObject.GetComponent<Player>()?.currentStats.currentForm == ScriptableStats.Form.Water)
        {
            SetState(State.Growing);

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && currentState != State.Retracting)
        {
            if (this.gameObject.activeSelf == true)
            if (!fixedTree)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                SetState(State.Retracting);
            }
        }
    }


    IEnumerator GrowTree()
    {
        while (Mathf.Abs(Vector2.Distance(currentPos.position, desiredPos.position)) > 0.1f)
        {
            transform.Translate(finalDirection * speed * Time.deltaTime);
            anim.SetTrigger("Shake");
            currentPos.position = transform.position;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            yield return null;
        }
    }

    IEnumerator RetractTree()
    {
        while (Mathf.Abs(Vector2.Distance(currentPos.position, startPos)) > 0.1f)
        {
            transform.Translate(finalDirection * -1 * speed * Time.deltaTime);
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


}
