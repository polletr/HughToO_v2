using UnityEngine;
[CreateAssetMenu]
public class PlayerBaseInfo : ScriptableObject
{
    public PlayerData Data;
}
[System.Serializable]
public class PlayerData 
{
    public float CurrentHealth;
    public float MaxHealth;

    public ScriptableStats.Form Currentform;

    public int CurrentScene;
    public float[] position;
    public  PlayerData (Player player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

    public bool HasIce;
    public bool HasWind;

}




