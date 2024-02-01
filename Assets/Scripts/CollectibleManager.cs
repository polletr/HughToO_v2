using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : Singleton<CollectibleManager>
{
    public int collectibleCount;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        collectibleCount = 0;
        audioSource = GetComponent<AudioSource>();
    }

    public void Add()
    {
        collectibleCount++;
        Debug.Log(collectibleCount);
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

}
