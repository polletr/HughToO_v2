using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class HurtCollider : MonoBehaviour, IDoDamage
{

    private int finalDamage;
    [SerializeField]
    private DamageScriptable scriptableAffordances;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private Player player;


    private void Start()
    {
        //playerHealth = playerHealth.GetComponent<PlayerHealth>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Damage Player");

            if (collision.transform.position.x <= transform.position.x)
            {
                player.currentStats.knockFromRight = true;
            }
            else
            {
                player.currentStats.knockFromRight = false;
            }

            player.ChangeState(new KnockBackState());
            DoDamage(collision.gameObject.GetComponent<Player>().currentStats);
        }
    }


    public void DoDamage(ScriptableStats playerStats)
    {
        checkAffordance(scriptableAffordances, playerStats);
        playerHealth.TakeDamage(finalDamage);
    }
    void checkAffordance(DamageScriptable affordanceStats, ScriptableStats playerStats)
    {
        switch (playerStats.currentForm)
        {
            case ScriptableStats.Form.Water:
                finalDamage = affordanceStats.waterDamage;
                break;
            case ScriptableStats.Form.Ice:
                finalDamage = affordanceStats.iceDamage;
                break;
            case ScriptableStats.Form.Gas:
                finalDamage = affordanceStats.gasDamage;
                break;
            default:
                break; 
        }
    }

}
