using UnityEngine.Events;

public class WinMenu : Menu
{
    public UnityEvent OnWin;
    private void Awake()
    {
        _startActive = true;
        AudioManager.Instance.PlayMusic(AudioManager.Instance._audioClip.WinBGMusic);
    }
    private void Start()
    {
        OnWin?.Invoke();
    }

    public void OnLoadWinLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("02_WinScreen");
    }
    public void OnLoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("00_MainMenu");
    }

}
