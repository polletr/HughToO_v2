using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CollectibleDataContainer : ScriptableObject
{
    public CollectibleData[] Heals;
    public CollectibleData power;
    
}
[System.Serializable]
public struct CollectibleData
{
    public GameObject collectible;
    public bool isCollected;
}
