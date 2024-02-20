using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRespawnWaterPos : Water
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            respawnPos = transform;
            Debug.Log(respawnPos.ToString());
        }
    }
}
