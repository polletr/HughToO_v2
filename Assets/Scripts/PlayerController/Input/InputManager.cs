using UnityEngine;
using UnityEngine.InputSystem;
using HughTo0;

public class InputManager : MonoBehaviour
{
    PlayerInput action;

    Player player { get; set; }

    void Awake()
    {
        player = GetComponent<Player>();
        action = new PlayerInput();
    }

    private void OnEnable() 
    {
       
        action.Player.Movement.performed += (val) => player.HandleMovement(val.ReadValue<Vector2>());
        action.Player.Attack.performed += (val) => player.HandleAttack();
        action.Player.Dash.performed += (val) => player.HandleDash();

        action.Player.Water.performed += (val) => player.HandleWater();
        action.Player.Ice.performed += (val) => player.HandleIce();
        action.Player.Wind.performed += (val) => player.HandleWind();


        action.Enable(); 
    }

    private void OnDisable()
    {
        action.Player.Movement.performed -= (val) => player.HandleMovement(val.ReadValue<Vector2>());
        action.Player.Attack.performed -= (val) => player.HandleAttack();
        action.Player.Dash.performed -= (val) => player.HandleDash();
        action.Player.Water.performed -= (val) => player.HandleWater();
        action.Player.Ice.performed -= (val) => player.HandleIce();
        action.Player.Wind.performed -= (val) => player.HandleWind();



        action.Disable(); 
    }

}
