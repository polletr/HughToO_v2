using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : Menu
{

    private void Awake()
    {
        _startActive = true;
    }
    public void OnPlayGame()
    {
        SceneManager.LoadScene("01_MainScene");
    }

}
