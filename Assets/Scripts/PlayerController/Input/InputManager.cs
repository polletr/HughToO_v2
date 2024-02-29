using UnityEngine;
using UnityEngine.InputSystem;
using HughTo0;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    PlayerInput action;

    Player player { get; set; }
    bool _isJumpHeldDown;
    public bool IsJumpHeldDown
    {
        get
        {
            return _isJumpHeldDown;
        }
        private set
        {
            _isJumpHeldDown = value;
        }
    }
    Vector2 _movement;
    public Vector2 Movement
    {
        get
        {
            return _movement;
        }
        private set
        {
            _movement = value;
        }
    }

    bool _isGliding;
    public bool IsGliding
    {
        get
        {
            return _isGliding;
        }
        private set
        {
            _isGliding = value;
        }
    }
    public float JumpButtonPressedLast
    {
        get; private set;
    }

    void Awake()
    {
        player = GetComponent<Player>();
        action = new PlayerInput();
    }

    private void Update()
    {
        var devices = InputSystem.devices;

        foreach (var device in devices)
        {
            // Check if the device is active
            if (device is Gamepad gamepad && gamepad.leftStick.ReadValue() != Vector2.zero)
            {
                UIGameControlsManager.Instance.SetToGamePadUI();
            }
            else if (device is Keyboard keyboard && (keyboard.anyKey.isPressed || keyboard.anyKey.wasPressedThisFrame))
            {
               UIGameControlsManager.Instance.SetToKeyBoardUI();
            }
        }

    }

    private void OnEnable()
    {

        action.Player.Movement.performed += (val) => Movement = val.ReadValue<Vector2>();

        action.Player.Attack.performed += (val) => player.HandleAttack();
        action.Player.Dash.performed += (val) => player.HandleDash();

        action.Player.Glid.performed += (val) => IsGliding = true;
        action.Player.Glid.canceled += (val) => IsGliding = false;


        action.Player.Jump.performed += (val) =>
        {
            JumpButtonPressedLast = Time.time;
            player.HandleJump();
            IsJumpHeldDown = true;
        };
        action.Player.Jump.canceled += (val) => IsJumpHeldDown = false;

        action.Player.Water.performed += (val) => player.HandleWater();
        action.Player.Ice.performed += (val) => player.HandleIce();
        action.Player.Wind.performed += (val) => player.HandleWind();


        action.Enable();
    }

    private void OnDisable()
    {
        action.Player.Movement.performed -= (val) => Movement = val.ReadValue<Vector2>();

        action.Player.Attack.performed -= (val) => player.HandleAttack();
        action.Player.Dash.performed -= (val) => player.HandleDash();

        action.Player.Glid.performed -= (val) => IsGliding = true;
        action.Player.Glid.canceled -= (val) => IsGliding = false;

        action.Player.Jump.performed -= (val) =>
        {
            JumpButtonPressedLast = Time.time;
            player.HandleJump();
            IsJumpHeldDown = true;
        };
        action.Player.Jump.canceled -= (val) => IsJumpHeldDown = false;

        action.Player.Water.performed -= (val) => player.HandleWater();
        action.Player.Ice.performed -= (val) => player.HandleIce();
        action.Player.Wind.performed -= (val) => player.HandleWind();




        action.Disable();
    }

    public void DisableForms()
    {
        action.Player.Water.Disable();
        action.Player.Ice.Disable();
        action.Player.Wind.Disable();
    }
    public void EnableForms()
    {
        action.Player.Water.Enable();
        action.Player.Ice.Enable();
        action.Player.Wind.Enable();
    }

}
