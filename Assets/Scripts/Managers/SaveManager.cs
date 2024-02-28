using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : Singleton<SaveManager>
{
    public PlayerBaseInfo PlayerData;
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
            Debug.Log("Loading save ");
            BinaryFormatter bf = new();
            FileStream stream = new(filePath, FileMode.Open);
            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + filePath);
            return null;
        }
    }
    public void SaveGame(PlayerData data)
    {
        Debug.Log("Saving game");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        bf.Serialize(stream, PlayerData.Data);
        stream.Close();
    }
/*
    private void OnApplicationQuit()
    {
        SaveGame(this.PlayerData.Data);
    }*/

    public void NewPlayerData()
    {
        PlayerData = BasePlayerData;
        SaveGame(PlayerData.Data);
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        SaveGame(PlayerData.Data);
    }
    public void LoadPlayerData()
    {
        PlayerData.Data = LoadSave();
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
