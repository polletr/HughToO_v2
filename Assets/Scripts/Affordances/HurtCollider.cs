using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour, IDoDamage
{

    private int finalDamage;
    [SerializeField]
    private DamageScriptable scriptableAffordances;

    private PlayerHealth playerHealth;
    private Player player;

    public UnityEvent Hurt;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();


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

            DoDamage(collision.gameObject.GetComponent<Player>().currentStats, playerHealth);
            Hurt.Invoke();
        }
    }


    public void DoDamage(ScriptableStats playerStats, PlayerHealth health)
    {
        checkAffordance(scriptableAffordances, playerStats);
        health?.TakeDamage(finalDamage);
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
