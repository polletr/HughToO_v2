using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HurtCollider : MonoBehaviour, IDoDamage
{
    public enum DamageType
    {
        Fire,
        Wind
    }

    public DamageType currentType;
    private int finalDamage;

    void checkAffordance(ScriptableStats stats)
    {
        switch (currentType)
        {
            case DamageType.Fire:
                finalDamage = stats.fireDamage;
                break;
            case DamageType.Wind:
                finalDamage = stats.windDamage;
                break;
            default:
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //DoDamage(collision.GetComponent<Player>().Scriptab);
        }
    }

    public void DoDamage(ScriptableStats form)
    {
        checkAffordance(form);
        //form.health -= finalDamage;
    }
}
