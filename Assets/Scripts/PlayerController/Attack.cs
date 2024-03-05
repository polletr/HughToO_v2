using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    Player player;


    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableObjects>())
        {
            ScriptableStats currentStats = player?.currentStats;
            Check(currentStats, collision.gameObject);// We need to get the current stats
        }
    }

    void Check(ScriptableStats stats, GameObject interactableObj)
    {
        InteractableObjects.ObjectType objType = interactableObj.GetComponent<InteractableObjects>().currentType;
        switch (objType)
        {
            case InteractableObjects.ObjectType.Fire:
                if (stats.currentForm == ScriptableStats.Form.Water)//Check if the State is Water
                {
                    interactableObj.GetComponent<InteractableObjects>().KillObj();
                }
                else if (stats.currentForm == ScriptableStats.Form.Ice)
                {
                    interactableObj.GetComponent<HurtCollider>().DoDamage(stats, player.GetComponent<PlayerHealth>()) ;
                    player.ChangeState(new KnockBackState());
                }
                else if (stats.currentForm == ScriptableStats.Form.Gas)
                {
                    interactableObj.GetComponent<InteractableObjects>().InteractObj();
                }
                break;
            case InteractableObjects.ObjectType.BreakableWall:
                if (stats.currentForm == ScriptableStats.Form.Ice) //Check if the State is Ice
                {
                    interactableObj.GetComponent<InteractableObjects>().KillObj();
                }
                break;
            case InteractableObjects.ObjectType.Enemy:
                if (stats.currentForm == ScriptableStats.Form.Gas)
                {
                    interactableObj.GetComponent<EnemyBase>().Stun();
                }
                else if (stats.currentForm == ScriptableStats.Form.Ice)
                {
                    interactableObj.GetComponent<EnemyBase>().TakeDamage(stats.damage);
                }
                break;
            case InteractableObjects.ObjectType.Mushroom:
                if (stats.currentForm == ScriptableStats.Form.Water)
                {
                    interactableObj.GetComponent<GrowingTree>().OnInteracted();
                    interactableObj.GetComponent<InteractableObjects>().InteractObj();
                }
                break;
            case InteractableObjects.ObjectType.Vine:
                if (stats.currentForm == ScriptableStats.Form.Water)
                {
                    interactableObj.GetComponent<GrowingVines>().OnInteracted();
                    interactableObj.GetComponent<InteractableObjects>().InteractObj();

                }
                break;
            case InteractableObjects.ObjectType.Bullet:
                if (stats.currentForm == ScriptableStats.Form.Gas)
                {
                    interactableObj.GetComponent<InteractableObjects>().InteractObj();

                }
                break;

            default:
                break;
        }
    }



}
