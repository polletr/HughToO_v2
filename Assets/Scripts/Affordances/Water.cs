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
    [HideInInspector]
    public Vector3 _teleportPosition;

    PlayerHealth player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerHealth>();

            if (player != null)
            {
                playerDrowned?.Invoke();
                StartCoroutine(StartTeleporting());
                player.TakeDamage(damage);

            }
        }
    }

    IEnumerator StartTeleporting()
    {
        yield return new WaitForSeconds(2f);
        if (!player.isDead)
        {
            SpawnPos?.Invoke(_teleportPosition);
        }
    }
}