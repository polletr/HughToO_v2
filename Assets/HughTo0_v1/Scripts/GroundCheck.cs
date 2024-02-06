using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float dieVelocity;

    public UnityEvent highFall;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && ClimateManager.Instance.currentState == ClimateManager.State.Ice && player.GetComponent<Rigidbody2D>().velocity.y < -dieVelocity)
        {
            highFall.Invoke();
        }
    }


}
