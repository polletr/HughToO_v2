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

    protected bool isStunned;
    protected float time;

    protected InteractableObjects interactableObj;

    [SerializeField]
    private float stunTime;

    protected Animator anim;


    public virtual void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        isAlive = true;

        enemyStats.currentHealth = enemyStats.maxHealth;

        rb = GetComponent<Rigidbody2D>();
        interactableObj = GetComponent<InteractableObjects>();
        time = 0;

    }

    public virtual void TakeDamage(int damage)
    {
        enemyStats.currentHealth -= damage;
        if (enemyStats.currentHealth <= 0)
            Die();

    }

    public virtual void Update()
    {
        time += Time.deltaTime;

        if (time >= stunTime && isStunned)
        {
            isStunned = false;
            time = 0;
        }
    }

    public virtual void Stun()
    {
        isStunned = true;
        time = 0;
    }

    public virtual void Die()
    {
        isAlive = false;

        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Collider2D>().enabled = false;

        interactableObj.KillObj();

        AudioManager.Instance.PlayEnemySFX(AudioManager.Instance._audioClip.EnemyDeath);
    }

}
