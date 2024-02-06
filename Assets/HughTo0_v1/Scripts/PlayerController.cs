using Percy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private ScriptableStats formStats;

    private float horizontal;
    private float moveSpeed;

    [SerializeField] private float waterSpeed;
    [SerializeField] private float gasSpeed;
    [SerializeField] private float iceSpeed;

    [SerializeField] private float jumpForce;

    private bool canJump;

    [SerializeField] private Sprite cloudSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite iceSprite;

    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private PhysicsMaterial2D waterMaterial;

    private CircleCollider2D waterCollider;
    private BoxCollider2D iceCollider;
    private PolygonCollider2D cloudCollider;

    [SerializeField] Transform teleportPosition;

    [SerializeField] float teleportDelay = 1.0f;

    [SerializeField] float iceMass;
    [SerializeField] float gasMass;
    [SerializeField] float waterMass;
    private bool isFacingRight;

    Dictionary<string, RuntimeAnimatorController> animControllers = new Dictionary<string, RuntimeAnimatorController>();

    public UnityEvent changeState;
    [SerializeField] float gizmoRadius = 0.5f; // Adjust this to change gizmo size

    private Animator anim;

    [SerializeField]
    private float dieVelocityWind;

    
    private AudioSource audioSource;
    [Header("Audio")]
    [SerializeField]
    private AudioClip fallWaterClip;
    [SerializeField]
    private AudioClip waterMovingClip;


    [SerializeField]
    private AudioClip breakIceClip;
    [SerializeField]
    private AudioClip iceMovingClip;
    [SerializeField]
    private AudioClip iceJumpingClip;

    [SerializeField]
    private AudioClip cloudMovingClip;
    [SerializeField]
    private AudioClip cloudDyingClip;

    private bool canMove;
    private void Start()
    {
    }
    // Start is called before the first frame update
    void Awake()
    {
        animControllers.Add("water_animator", Resources.Load("Animations/Player/Water/Water_Player") as RuntimeAnimatorController);
        animControllers.Add("ice_animator", Resources.Load("Animations/Player/Ice/Ice_Player") as RuntimeAnimatorController);
        animControllers.Add("gas_animator", Resources.Load("Animations/Player/Gas/Gas_Player") as RuntimeAnimatorController);

        audioSource = GetComponent<AudioSource>();
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        waterCollider = GetComponent<CircleCollider2D>();
        iceCollider = GetComponent<BoxCollider2D>();
        cloudCollider = GetComponent<PolygonCollider2D>();
    }

    public void OnWater()
    {
        anim.runtimeAnimatorController = animControllers["water_animator"]; 
        changeState?.Invoke();
        moveSpeed = waterSpeed;
        rb.gravityScale = 0.5f;
        rb.sharedMaterial = waterMaterial;
        rb.mass = waterMass;

        iceCollider.enabled = false;
        cloudCollider.enabled = false;
        waterCollider.enabled = true;

        canJump = false;
        playerSprite.sprite = waterSprite;
    }

    public void OnIce()
    {
        anim.runtimeAnimatorController = animControllers["ice_animator"];

        changeState?.Invoke();
        moveSpeed = iceSpeed;
        rb.gravityScale = 1f;
        rb.angularDrag = 0.2f;
        rb.sharedMaterial = iceMaterial;
        rb.mass = iceMass;

        iceCollider.enabled = true;
        cloudCollider.enabled = false;
        waterCollider.enabled = false;


        canJump = true;
        playerSprite.sprite = iceSprite;

    }

    public void OnGas()
    {
        anim.runtimeAnimatorController = animControllers["gas_animator"];

        changeState?.Invoke();
        moveSpeed = gasSpeed;
        rb.gravityScale = 0f;
        rb.mass = gasMass;
        playerSprite.sprite = cloudSprite;
        rb.velocity = new Vector2(0, 0);
        iceCollider.enabled = false;
        cloudCollider.enabled = true;
        waterCollider.enabled = false;

        canJump = false;

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Update()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim?.SetTrigger("Jumping");
            audioSource.clip = iceJumpingClip;
            audioSource.Play();


        }
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Flip();
        if (ClimateManager.Instance.currentState != ClimateManager.State.Gas)
        {
            if (!IsGrounded())
            {
                anim.SetBool("Falling", true);
            }
            else
            {
                anim.SetBool("Falling", false);
            }

        }

        if (canMove)
        {

            if (horizontal != 0)
            {
                anim.SetBool("Moving", true);
            }
            else
            {
                anim.SetBool("Moving", false);
            }

            if (ClimateManager.Instance.currentState == ClimateManager.State.Ice)
            {
                rb.AddForce(new Vector2(moveSpeed * horizontal, 0), ForceMode2D.Force);
            }
            else
            {
                rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            }


        }



    }

    private void Flip()
    {
        if (isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ClimateManager.Instance.currentState == ClimateManager.State.Gas && collision.gameObject.tag != "Wind" && (Mathf.Abs(rb.velocity.y) > dieVelocityWind || Mathf.Abs(rb.velocity.x) > dieVelocityWind))
        {
            anim.SetTrigger("Die");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            teleportPosition.position = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }

    public void Die()
    {
        canMove = false;
        if (ClimateManager.Instance.currentState == ClimateManager.State.Ice)
        {
            StopMovement();
            audioSource.clip = breakIceClip;
            audioSource.Play();
            anim.SetTrigger("Die");

        }
        Invoke("TeleportWithDelay", teleportDelay);
    }

    public void StopMovement()
    {
        canMove = false;
        rb.velocity = new Vector2 (0f, 0f);
    }

    public void RestartMovement()
    {
        canMove = true;

    }

    private void TeleportWithDelay()
    {
        ClimateManager.Instance.SetState(ClimateManager.State.Water);

        this.transform.position = teleportPosition.position;
        RestartMovement();
    }

    private void OnDrawGizmos()
    {
        // Draw a gizmo sphere to visualize the teleport position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(teleportPosition.position, gizmoRadius);
    }

    public void BackToWater()
    {
            ClimateManager.Instance.SetState(ClimateManager.State.Water);
    }

    public void FallWaterSound()
    {
        audioSource.clip = fallWaterClip;
        audioSource.Play();
    }

    public void PlayMoveSound()
    {
        if (ClimateManager.Instance.currentState == ClimateManager.State.Water)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = waterMovingClip;
                audioSource.Play();
            }
        }
        if (ClimateManager.Instance.currentState == ClimateManager.State.Ice)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = iceMovingClip;
                audioSource.Play();
            }
        }
        if (ClimateManager.Instance.currentState == ClimateManager.State.Gas)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = cloudMovingClip;
                audioSource.Play();
            }
        }

    }
}
