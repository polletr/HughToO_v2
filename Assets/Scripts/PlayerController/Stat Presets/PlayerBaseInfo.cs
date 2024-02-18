using UnityEngine;
[CreateAssetMenu]
public class PlayerBaseInfo : ScriptableObject
{
    public float CurrentHealth;
    public float MaxHealth;

    public ScriptableStats.Form Currentform;

    public int CurrentScene;
    public Transform CurrentCheckpoint;

    public bool HasIce;
    public bool HasWind;

}
