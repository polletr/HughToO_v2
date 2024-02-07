using UnityEngine;

    [CreateAssetMenu]
    public class EnemyStats : ScriptableObject
    {
        [Header("LAYERS")]
        public LayerMask EnemyLayer;

        [Header("INPUT")]

        public int currentHealth;

        public int maxHealth;

        public int damage;

    }
