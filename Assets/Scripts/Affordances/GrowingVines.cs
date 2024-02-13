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

    private BoxCollider2D boxCollider;

    private Stack<GameObject> vineStack = new Stack<GameObject>();

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
        boxCollider = GetComponent<BoxCollider2D>();
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
            
            StartCoroutine(RetractVines());

        }

    }

    IEnumerator GrowVines()
    {
        while (Mathf.Abs(Vector2.Distance(currentPos, desiredPos.position)) > 0.1f)
        {
            transform.Translate(finalDirection * speed * Time.fixedDeltaTime);
            currentPos = transform.position;
            float distanceTraveled = Mathf.Abs(currentPos.x - startPos.x);
            if (distanceTraveled % objectSize <= 0.01f )
            {
                boxCollider.size = new Vector2(distanceTraveled + objectSize, boxCollider.size.y);
                boxCollider.offset = new Vector2(-(distanceTraveled)/2, boxCollider.offset.y);
                GameObject vine = Instantiate(VinePrefab, startPos, Quaternion.identity, this.transform);
                vineStack.Push(vine);
            }
            yield return null;
        }
    }

    IEnumerator RetractVines()
    {
        boxCollider.size = new Vector2(objectSize, boxCollider.size.y);
        boxCollider.offset = new Vector2(0f, boxCollider.offset.y);

        while (Mathf.Abs(Vector2.Distance(currentPos, startPos)) > 0.1f)
        {
            transform.Translate(finalDirection * -1 * speed * Time.fixedDeltaTime);
            currentPos = transform.position;
            float distanceTraveled = Mathf.Abs(currentPos.x - startPos.x);
            Debug.Log(distanceTraveled);
            if (distanceTraveled % objectSize <= 0.01f)
            {
                Destroy(vineStack.Pop());
                Debug.Log("Destroy");
            }
            yield return null;
        }

    }


}

