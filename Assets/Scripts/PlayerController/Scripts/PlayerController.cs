using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;


    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;


    [SerializeField] private Transform teleportPosition;

    private float horizontal;
    private float moveSpeed;

    private bool isFacingRight;

    Dictionary<string, RuntimeAnimatorController> animControllers = new Dictionary<string, RuntimeAnimatorController>();

    public UnityEvent changeState;
    [SerializeField] float gizmoRadius = 0.5f; // Adjust this to change gizmo size

    private Animator anim;

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
    }

   

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {

       // Flip();


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

  /*  public void StopMovement()
    {
        rb.velocity = new Vector2 (0f, 0f);
    }
*/
  

   /* private void TeleportWithDelay()
    {
        ClimateManager.Instance.SetState(ClimateManager.State.Water);

        this.transform.position = teleportPosition.position;
      
    }*/

    private void OnDrawGizmos()
    {
        // Draw a gizmo sphere to visualize the teleport position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(teleportPosition.position, gizmoRadius);
    }

   
}
