using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject spikePrefab; // Reference to the spike GameObject
    [SerializeField]
    Transform spawnPoint; // Point where spikes will spawn

    float time;
    [SerializeField]
    float interval = 10f;

    private void Start()
    {
        time = 0;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interval) 
        {
            time = 0;
            ShootSpike();
        }
    }

    void ShootSpike()
    {
        GameObject newSpike = Instantiate(spikePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
