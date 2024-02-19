using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public enum Type
    {
        Fire,
        BreakableWall,
        Enemy
    }

    public Type currentType;


    void Check(ScriptableStats stats)
    {
        switch (currentType)
        {
            case Type.Fire:
                if (stats.currentForm == ScriptableStats.Form.Water)//Check if the State is Water
                {
                 //Play fire animation dissipating
                 //Destroy this gameobject after animation
                }

                break;
            case Type.BreakableWall:
                if (stats.currentForm == ScriptableStats.Form.Ice) //Check if the State is Ice
                {
                 //Play wall animation breaking
                 //Destroy this gameobject after animation
                }
                break;
            case Type.Enemy:
                //Play Enemy got hit animation
                this.gameObject.GetComponent<EnemyBase>().TakeDamage(stats.damage);
                if (stats.currentForm == ScriptableStats.Form.Gas)
                {
                    //push enemy
                }
                break;
            default:
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            Check(collision.GetComponentInParent<Player>().currentStats);// We need to get the current stats
        }
    }

}
