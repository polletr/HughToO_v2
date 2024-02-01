using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    public UnityEvent playerDrowned;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            playerDrowned.Invoke();
        }

    }
}