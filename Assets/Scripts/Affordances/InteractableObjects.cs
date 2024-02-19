using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObjects : MonoBehaviour
{
    public UnityEvent KillObject;
    public UnityEvent InteractObject;

    public enum ObjectType
    {
        Fire,
        BreakableWall,
        Enemy
    }

    public ObjectType currentType;

    public void KillObj()
    {
        KillObject?.Invoke();
        //Play Animation
        //Play Sound
        //GetComponent<Collider2D>().enabled = false;
    }

    public void InteractObj()
    {
        InteractObject?.Invoke();
        //Play Animation
        //Play Sound
    }


}
