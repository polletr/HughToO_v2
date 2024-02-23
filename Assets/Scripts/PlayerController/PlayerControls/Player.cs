using HughTo0;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(InputManager))]
public class Player : MonoBehaviour
{
    /*public PlayerState State { get; private set; }
    public PlayerStateType StateType;

    private Dictionary<PlayerStateType, PlayerState> playerStates;*/
    public PlayerState currentState;

    public PlayerBaseInfo playerData;

    public Transform _groundCheckPos;

    public ScriptableStats[] stats;
    public ScriptableStats currentStats;

    public GameObject AttackHitBox;

    private InputManager inputManager;

    private Vector2 _moveInput;
    public bool _isMoving, _isFalling, _isSprinting, _isJumping, _isGliding, _isAttacking, _isDashing, _isWater, _isIce, _isWind;

    public Vector3 Position => transform.position;

    public Rigidbody2D _rb;
    private Collider2D _collider;

    [SerializeField]
    private float MaxFallingTime = 0.5f;
    private float falltimer;

    public bool canDash = false;
    public bool isDashing = false;
    public float dashCooldown = 10f;
    public float dashTimer = 0f;

    Dictionary<string, RuntimeAnimatorController> animControllers = new Dictionary<string, RuntimeAnimatorController>();
    private Animator anim;



    private void Awake()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].currentForm.ToString() == "Water")
            {
                animControllers.Add("Water", stats[i].formAnimator);
            }
            else if (stats[i].currentForm.ToString() == "Ice")
            {
                animControllers.Add("Ice", stats[i].formAnimator);
            }
            else if (stats[i].currentForm.ToString() == "Gas")
            {
                animControllers.Add("Gas", stats[i].formAnimator);
            }

        }

        playerData.Data.position[0] = transform.position.x;
        playerData.Data.position[1] = transform.position.y;
        playerData.Data.position[2] = transform.position.z;

        anim = GetComponent<Animator>();

        inputManager = GetComponent<InputManager>();

        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        //_animator = GetComponent<Animator>();
        AttackHitBox.SetActive(false);
        ChangeState(new IdleState());

    }

    private void Update()
    {
        currentState?.StateUpdate();
       // Debug.Log(currentState.ToString());
        if (canDash)
        {
            canDash = false;
            isDashing = true;
            dashTimer = 0f;
            if (dashTimer >= dashCooldown)
            {
                canDash = true;
                isDashing = false;
            }
        }
        dashTimer += Time.deltaTime;
        FallDamageCheck();
    }
    private void FixedUpdate()
    {
        currentState?.StateFixedUpdate();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.player = this;
        currentState.inputManager = inputManager;
        currentState.EnterState();
    }

    #region Player Actions
    public void HandleJump()
    {
        currentState?.HandleJump();
    }
    public void HandleAttack()
    {
        if (GroundCheck())
        {
            ChangeState(new AttackState());
        }
    }


    public void HandleDash()
    {
        if (canDash)
        {
            ChangeState(new DashState());
        }
    }
    public void HandlePotatoState(float time)
    {
        ChangeState(new PotatoState());
        if (currentState is PotatoState state)
            state._potatoTime = time;
    }
    public void HandleWater()
    {
        canDash = false;
        ChangeForm(ScriptableStats.Form.Water);
    }

    public void HandleIce()
    {
        canDash = false;
        if (playerData.Data.HasIce)
            ChangeForm(ScriptableStats.Form.Ice);
    }

    public void HandleWind()
    {
        canDash = true;
        if (playerData.Data.HasWind)
            ChangeForm(ScriptableStats.Form.Gas);
    }
    #endregion
    void ChangeForm(ScriptableStats.Form newForm)
    {
        foreach (ScriptableStats stat in stats)
        {
            if (stat.currentForm == newForm)
            {
                currentStats = stat;
                anim.runtimeAnimatorController = animControllers[newForm.ToString()];
            }
        }
        playerData.Data.Currentform = newForm;
    }

    public void GetIceForm()
    {
        playerData.Data.HasIce = true;
    }

    public void GetWindForm()
    {
        playerData.Data.HasWind = true;
    }

    void FallDamageCheck()
    {
        bool isGrounded = GroundCheck();
        bool isFallingToDamage = MaxFallingTime < falltimer ;

        if (!isGrounded)
        {
            falltimer += Time.deltaTime;
            //Debug.Log("InAirTime: " + falltimer);
        }
        else if (isGrounded)
        {
            if (isFallingToDamage && currentStats.currentForm == ScriptableStats.Form.Ice)
            {
                GetComponent<PlayerHealth>()?.TakeDamage(Mathf.RoundToInt(falltimer/MaxFallingTime));
               // Debug.Log("FallDamage" + Mathf.RoundToInt(falltimer / MaxFallingTime));
            }
            falltimer = 0;
        }
    }
    public bool GroundCheck()
    {
        return Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, LayerMask.GetMask("Ground"));
    }
}


