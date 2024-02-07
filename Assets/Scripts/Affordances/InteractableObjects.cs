using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public enum Type
    {
        Fire,
        BreakableWall
    }

    public Type currentType;

    private bool checkAttack = false;

    void Check(ScriptableStats stats)
    {
        switch (currentType)
        {
            case Type.Fire:
                if (true)//Check if the State is Water
                    checkAttack = true; 
                break;
            case Type.BreakableWall:
                if (true)//Check if the State is Ice
                    checkAttack = true;
                break;
            default:
                break;
        }
        if (checkAttack)
        {
            //Run Animation and Destroy game object at the end of animation
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            //Check(collision.GetComponentInParent<PlayerController>().stats); We need to get the current stats
        }
    }

}
