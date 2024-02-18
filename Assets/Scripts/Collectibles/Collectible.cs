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
        Destroy(gameObject);
    }

}
