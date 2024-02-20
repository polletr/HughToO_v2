using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{

    public UnityEvent playerDrowned;
    public UnityEvent<Vector3> SpawnPos;


    [SerializeField]
    private int damage;
    public Vector3 _teleportPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDrowned?.Invoke();
            SpawnPos?.Invoke(_teleportPosition);
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);

        }
    }
}