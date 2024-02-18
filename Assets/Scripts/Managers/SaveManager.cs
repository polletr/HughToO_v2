using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public PlayerBaseInfo playerInfo;
    private string saveFileName = "Save.json";
    public string saveFolderName = "/SaveFolder";

    private string savePath;
    private string baseSavePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.dataPath, saveFolderName, saveFileName);
        string json = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(savePath, json);

        if (baseSavePath == null)
        {
            baseSavePath = savePath;
            string json2 = JsonUtility.ToJson(playerInfo);
            File.WriteAllText(baseSavePath, json2);
            Debug.Log("Base save file created at: " + baseSavePath);
        }
        else
        {
            Debug.LogError("Base save file already exists at: " + baseSavePath);
        }
    }

    public void NewGame()
    {
        if (File.Exists(baseSavePath))
        {
            string json = File.ReadAllText(baseSavePath);
            playerInfo = JsonUtility.FromJson<PlayerBaseInfo>(json);
            savePath = Path.Combine(Application.dataPath, saveFolderName, saveFileName);
            string json2 = JsonUtility.ToJson(playerInfo);
            File.WriteAllText(savePath, json2);
            Debug.Log("New game started. Save file created at: " + savePath);
        }
        else
        {
            Debug.LogError("Base save file does not exist at: " + baseSavePath);
        }
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(savePath, json);
    }

    public void LoadSave()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            playerInfo = JsonUtility.FromJson<PlayerBaseInfo>(json);
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
}
