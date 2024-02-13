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
        startPos = new Vector2(currentPos.position.x, currentPos.position.y);
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

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" /*&& Add Check for water form*/)
        {
            StopAllCoroutines();
            StartCoroutine(GrowTree());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!fixedTree && other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(RetractTree());
        }

    }

    IEnumerator GrowTree()
    {
        while (Vector2.Distance(currentPos.position, desiredPos.position) > 0.01)
        {
            transform.Translate(finalDirection * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            currentPos = transform;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        yield return null;
    }

    IEnumerator RetractTree()
    {
        while (Vector2.Distance(currentPos.position, startPos) > 0.01)
        {
            transform.Translate(finalDirection * -1 * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            currentPos = transform;
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
