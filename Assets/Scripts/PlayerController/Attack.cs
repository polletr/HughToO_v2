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
        if (collision.CompareTag("Interactable"))
        {
            ScriptableStats currentStats = player.currentStats;
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
                    //Play fire animation dissipating
                    //Destroy this gameobject after animation
                }
                else if (stats.currentForm == ScriptableStats.Form.Ice)
                {
                    interactableObj.GetComponent<HurtCollider>().DoDamage(stats);
                    player.ChangeState(new KnockBackState());
                }
                else if (stats.currentForm == ScriptableStats.Form.Gas)
                {
                    //Play Fire blowing animation
                    //Play Sound
                }
                break;
            case InteractableObjects.ObjectType.BreakableWall:
                if (stats.currentForm == ScriptableStats.Form.Ice) //Check if the State is Ice
                {
                    //Play wall animation breaking
                    //Destroy this gameobject after animation
                }
                break;
            case InteractableObjects.ObjectType.Enemy:
                //Play Enemy got hit animation
                this.gameObject.GetComponent<EnemyBase>().TakeDamage(stats.damage);
                if (stats.currentForm == ScriptableStats.Form.Gas)
                {
                    //push enemy
                }
                else
                {
                    //Do Damage to Enemy
                }
                break;
            default:
                break;
        }
    }



}
