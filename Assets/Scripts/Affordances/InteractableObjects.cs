using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public enum ObjectType
    {
        Fire,
        BreakableWall,
        Enemy
    }

    public ObjectType currentType;

    private void Start()
    {
        
    }

    public void DestroyObject()
    {
        //Play Animation
        //Play Sound
        Destroy(gameObject, 1f);
    }



}
