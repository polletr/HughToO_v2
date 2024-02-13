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
        Up,
        Left,
        Right
    }

    public growthDirection currentDirection;
    private Vector2 finalDirection;

    [SerializeField]
    private float originalSpeed;

    private float speed = 0f;

    [SerializeField]
    private Transform desiredPos;

    [SerializeField]
    private Transform currentPos;

    private Vector2 startPos;

    private bool growing;

    private float timer;

    [SerializeField]
    private bool fixedTree;

    private Animator anim;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
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
            case growthDirection.Left:
                finalDirection = Vector2.left;
                break;
            case growthDirection.Right:
                finalDirection = Vector2.right;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (growing && currentPos.position.y < desiredPos.position.y)
        {
            transform.Translate(finalDirection * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else if (!fixedTree && !growing && currentPos.position.y > startPos.y)
        {
            transform.Translate(finalDirection * speed * Time.fixedDeltaTime);
            anim.SetTrigger("Shake");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }


        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && ClimateManager.Instance.currentState == 0)
        {
            growing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!fixedTree)
        {
            growing = false;

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
