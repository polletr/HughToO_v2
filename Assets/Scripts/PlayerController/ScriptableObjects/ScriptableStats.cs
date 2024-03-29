using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableStats : ScriptableObject
{
    public enum Form
    {
        Water,
        Ice,
        Gas
    }

    [Header("LAYERS")]
    public LayerMask PlayerLayer;

    [Header("FORM")]
    public Form currentForm;

    [Header("ANIMATOR")]
    public RuntimeAnimatorController formAnimator;

    [Header("COMBAT")]
    public int damage;

    [Header("INPUT")]
    public bool SnapInput = true;

    public float VerticalDeadZoneThreshold = 0.3f;

    public float HorizontalDeadZoneThreshold = 0.1f;

    public float MaxSpeed = 14;

    public float DashSpeed = 20;

    public float Acceleration = 120;

    public float GroundDeceleration = 60;

    public float AirDeceleration = 30;

    public float GroundingForce = -1.5f;

    public float GrounderDistance = 0.05f;

    public float JumpPower = 36;

    public float MaxFallSpeed = 40;

    public float FallAcceleration = 110;

    public float JumpEndEarlyGravityModifier = 3;

    public float CoyoteTime = .15f;

    public float JumpBuffer = .2f;

    public float KBForce = 5f;

    public float KBCounter = .2f;

    [HideInInspector]
    public bool knockFromRight;//hide in inspector

    public float KBFallAcceleration;

    public int FallDamage = 0;

    public float GlideFallSpeed = 3f;

}
