using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class ClimateManager : Singleton<ClimateManager>
{

    public enum State
    {
        Water,
        Gas,
        Ice
    }

    public State currentState; //this keeps track of the current state

    public UnityEvent IceState;
    public UnityEvent GasState;
    public UnityEvent WaterState;

    public void SetState(State newState)
    {
        currentState = newState;
        StopAllCoroutines();//stop the previous coroutines so they aren't operating at the same time

        switch (currentState)
        {
            case State.Water:
                WaterState?.Invoke();
                //do some work
                break;
            case State.Gas:
                GasState?.Invoke();
                //do some work
                break;
            case State.Ice:
                IceState?.Invoke();
                //do some work
                break;
            default:
                break;
        }
        ///
    }

    // Start is called before the first frame update
    void Start()
    {
            SetState(State.Water);

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Keypad1)) && currentState != State.Water)
        {
            SetState(State.Water);
        }
        if ((Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Keypad2)) && currentState != State.Ice && SceneManager.GetActiveScene().buildIndex >= 2)
        {
            SetState(State.Ice);
        } 
        if ((Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Keypad3)) && currentState != State.Gas && SceneManager.GetActiveScene().buildIndex >= 3)
        {
            SetState(State.Gas);
        }

    }
}
