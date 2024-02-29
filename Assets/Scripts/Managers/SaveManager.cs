using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : Singleton<SaveManager>
{
    public PlayerBaseInfo playerData;
    public PlayerBaseInfo BasePlayerData;


    private string filePath;
    private string fileName = "/SaveData.meow"; 
    
    private void Awake()
    {
         filePath = Application.persistentDataPath + fileName;
        LoadSave();
    }

    public PlayerData LoadSave()
    {
        if (File.Exists(filePath))
        {
           // Debug.Log("Loading save ");
            BinaryFormatter bf = new();
            FileStream stream = new(filePath, FileMode.Open);
            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("Save file not found in " + filePath);
            return null;
        }
    }
    public void SaveGame(PlayerData data)
    {
       // Debug.Log("Saving game");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        bf.Serialize(stream, playerData.Data);
        stream.Close();
    }
/*
    private void OnApplicationQuit()
    {
        SaveGame(this.PlayerData.Data);
    }*/

    public void NewPlayerData()
    {
       // playerData = BasePlayerData;
        playerData.Data.MaxHealth = BasePlayerData.Data.MaxHealth;
        playerData.Data.CurrentHealth = BasePlayerData.Data.MaxHealth;
        playerData.Data.HasIce = BasePlayerData.Data.HasIce;
        playerData.Data.HasWind = BasePlayerData.Data.HasWind;
        playerData.Data.position[0] = BasePlayerData.Data.position[0];
        playerData.Data.position[1] = BasePlayerData.Data.position[1];
        playerData.Data.position[2] = BasePlayerData.Data.position[2];

        Debug.Log("New Player Data");
        SaveGame(playerData.Data);
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        SaveGame(playerData.Data);
    }
    public void LoadPlayerData()
    {
        playerData.Data = LoadSave();
    }
    private void Update()
    {
        //debugging
        /*Debug.Log("Player Health = " + PlayerData.Data.CurrentHealth);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveGame(PlayerData.Data);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadPlayerData();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerData.Data.CurrentHealth = 100;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerData.Data.CurrentHealth = 0;
        }*/
    }
}
