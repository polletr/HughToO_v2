using HughTo0;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(InputManager))]
public class Player : MonoBehaviour
{
    /*public PlayerState State { get; private set; }
    public PlayerStateType StateType;

    private Dictionary<PlayerStateType, PlayerState> playerStates;*/
    PlayerState currentState;
    PlayerStateType currentStateValue;

    public Transform _groundCheckPos;

    public ScriptableStats[] stats;
    public ScriptableStats currentStats;

    public Collider2D AttackHitBox;

    private InputManager inputManager;

    private Vector2 _moveInput;
    public bool _isMoving, _isFalling, _isSprinting, _isJumping, _isGliding, _isAttacking, _isDashing, _isWater, _isIce, _isWind;

    public Vector3 Position => transform.position;
    public Vector2 Velocity
    {
        get => _rb.velocity;
        set => _rb.velocity = value;
    }


    private Animator _animator;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();

        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        //_animator = GetComponent<Animator>();
        AttackHitBox.enabled = false;
        ChangeState(new IdleState());

    }

    private void Update()
    {
        currentState?.StateUpdate();
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

    public void HandleAttack()
    {
        if (GroundCheck())
        {
            ChangeState(new AttackState());
            currentState?.OnAttack();
        }
    }

    public void HandleDash()
    {

    }

    public void HandleWater()
    {
        ChangeForm(ScriptableStats.Form.Water);
    }

    public void HandleIce()
    {
        ChangeForm(ScriptableStats.Form.Ice);
    }

    public void HandleWind()
    {
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
            }
        }
    }


    public bool GroundCheck()
    {
        return Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, LayerMask.GetMask("Ground"));
    }
}


