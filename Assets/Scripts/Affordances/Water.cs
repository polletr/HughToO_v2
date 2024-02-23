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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDrowned?.Invoke();
            StartCoroutine(StartTeleporting());
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);

        }
    }

    IEnumerator StartTeleporting()
    {
        yield return new WaitForSeconds(2f);
        SpawnPos?.Invoke(_teleportPosition);
    }
}