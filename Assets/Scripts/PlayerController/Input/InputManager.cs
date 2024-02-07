using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  

public class InputManager : MonoBehaviour
{
    public PlayerInputs action;

    public InputAction Movement;
    public InputAction Jump;
    public InputAction Glid;
    public InputAction Attack;
    public InputAction Dash;

    public InputAction WaterForm;
    public InputAction IceForm;
    public InputAction WindForm;

    void Awake()
    {
        action = new PlayerInputs();
     
        Movement = action.Player.Movement;
        Jump = action.Player.Jump;
        Glid = action.Player.Glid;
        Attack = action.Player.Attack;
        Dash = action.Player.Dash;

        WaterForm = action.Player.Water;
        IceForm = action.Player.Ice;
        WindForm = action.Player.Wind;
    }

    private void OnEnable() => action.Enable();

    private void OnDisable() => action.Disable();
}
