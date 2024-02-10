using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HughTo0;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(InputManager))]
public class Player : MonoBehaviour
{
    /*public PlayerState State { get; private set; }
    public PlayerStateType StateType;

    private Dictionary<PlayerStateType, PlayerState> playerStates;*/
    PlayerState currentState;
    PlayerStateType currentStateValue;

    public Transform GroundCheck;

    public ScriptableStats stats;

    private InputManager InputManager;

    private Vector2 _moveInput;
    private bool _isMoving, _isFalling, _isSprinting, _isJumping, _isGliding, _isAttacking, _isDashing, _isWater, _isIce, _isWind;

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
        InputManager = GetComponent<InputManager>();

        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        //_animator = GetComponent<Animator>();

        ChangeState(new InAirState());

    }

    private void Start()
    {

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
        currentState.EnterState();
    }

    #region Player Actions
    public void HandleMovement(Vector2 movement)
    {
        currentState?.OnMovement(movement);
    }

    public void HandleGlid()
    {

    }
    public void HandleAttack()
    {

    }

    public void HandleDash()
    {

    }

    public void HandleWater()
    {

    }

    public void HandleIce()
    {

    }

    public void HandleWind()
    {

    }
    #endregion

}


