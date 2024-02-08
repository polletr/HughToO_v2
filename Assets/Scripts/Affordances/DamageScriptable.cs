using UnityEngine;

    [CreateAssetMenu]
    public class DamageScriptable : ScriptableObject
    {
        [Header("LAYERS")]
        public LayerMask AffordanceLayer;

        [Header("INPUT")]

        public int iceDamage;
        public int waterDamage;
        public int gasDamage;

    }
