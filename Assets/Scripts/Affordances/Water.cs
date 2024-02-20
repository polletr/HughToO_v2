using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{

    public UnityEvent playerDrowned;

    [SerializeField]
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDrowned?.Invoke();
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}