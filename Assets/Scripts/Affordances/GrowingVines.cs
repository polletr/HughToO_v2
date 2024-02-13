using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingVines : MonoBehaviour
{
    [SerializeField]
    private GameObject VinePrefab;
    public enum growthDirection
    {
        Left,
        Right
    }

    public growthDirection currentDirection;
    private Vector2 finalDirection;

    [SerializeField]
    private float originalSpeed;

    private float speed = 0f;

    private SpriteRenderer spriteRenderer; 

    [SerializeField]
    private Transform desiredPos;

    private Vector2
        currentPos;

    private Vector2 startPos;

    private bool growing;

    private float timer;

    [SerializeField]
    private bool fixedTree;

    private Animator anim;

    private float objectSize;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
        currentPos = transform.position;
        startPos = new Vector2(currentPos.x, currentPos.y);
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        GetVector(currentDirection);

        objectSize = spriteRenderer.bounds.size.x;
    }

    private void GetVector(growthDirection direction)
    {
        switch (direction)
        {
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

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")  /* && Add condition for being water*/)
        {
            other.transform.SetParent(transform);

            StartCoroutine(GrowVines());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")  /* && Add condition for being water*/)
        {
            other.transform.SetParent(null);
        }

        if (!fixedTree)
        {
            growing = false;

        }

    }

    IEnumerator GrowVines()
    {
        while (Vector2.Distance(currentPos, desiredPos.position) > 0.1f)
        {
            transform.Translate(finalDirection * speed * Time.fixedDeltaTime);
            currentPos = transform.position;
            float distanceTraveled = Mathf.Abs(currentPos.x - startPos.x);
            if (distanceTraveled % objectSize <= 0.1f )
            {
               Instantiate(VinePrefab, startPos, Quaternion.identity, this.transform);
                Debug.Log("Instantiate");
            }
            yield return null;
        }
    }

}

