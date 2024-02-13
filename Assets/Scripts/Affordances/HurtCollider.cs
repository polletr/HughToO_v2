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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Damage Player");
            //DoDamage(collision.GetComponent<Player>().Scriptab);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Damage Player");
            //DoDamage(collision.GetComponent<Player>().Scriptab);
        }
    }


    public void DoDamage(ScriptableStats playerStats)
    {
        checkAffordance(scriptableAffordances, playerStats);
        //playerStats.health -= finalDamage;
    }
}
