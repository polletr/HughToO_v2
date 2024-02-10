using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected DamageScriptable scriptableAffordance;
    [SerializeField]
    protected EnemyStats enemyStats;

    protected bool isAlive;
    protected Rigidbody2D rb;

    public virtual void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
        isAlive = true;

        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(int damage)
    {
        enemyStats.currentHealth -= damage;
        if (enemyStats.currentHealth <= 0)
            Die();

    }

    public virtual void Die()
    {
        isAlive = false;
        //animator.SetBool("IsAlive", false);
        //animator.SetTrigger("Death");
    }

}
