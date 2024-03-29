using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : Menu
{
    [SerializeField] private Button _loadGameButton;
    private void Awake()
    {
        Time.timeScale = 1f;
        _startActive = true;
        SetLoadGameActive();
        AudioManager.Instance.PlayMusic(AudioManager.Instance._audioClip.MainMenu);
    }
    public void OnLoadGame()
    {
        SaveManager.Instance.LoadPlayerData();
        SceneManager.LoadScene(1);
    }
    public void OnNewGame()
    {
        SaveManager.Instance.NewPlayerData();
        SceneManager.LoadScene(1);
    }
       public  void SetLoadGameActive()
    {
        if (SaveManager.Instance.LoadSave() != null)
        {
            //Debug.Log("Save file found");
            _loadGameButton.interactable = true;
        }
        else
        {
            //Debug.Log("Save file not found");
            _loadGameButton.interactable = false;
        }
    }

}
