using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyBase
{
    [SerializeField]
    GameObject spikePrefab; // Reference to the spike GameObject
    [SerializeField]
    Transform spawnPoint; // Point where spikes will spawn

    [SerializeField]
    float interval = 10f;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if (time >= interval && !isStunned && isAlive) 
        {
            time = 0;
            ShootSpike();
        }
    }

    void ShootSpike()
    {
        anim.SetTrigger("Shoot");
        AudioManager.Instance.PlayEnemySFX(AudioManager.Instance._audioClip.EnemyShoot);
        GameObject newSpike = Instantiate(spikePrefab, spawnPoint.position, spawnPoint.rotation);

    }
}
