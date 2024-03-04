using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Collectible : MonoBehaviour, ICollectible
{
    public UnityEvent OnCollected;
    public void Collect()
    {
        OnCollected?.Invoke();
        AudioManager.Instance.PlayWorldSFX(AudioManager.Instance._audioClip.CollectedPickup);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }


}
