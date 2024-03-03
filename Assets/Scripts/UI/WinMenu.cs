using UnityEngine.Events;

public class WinMenu : Menu
{
    public UnityEvent OnWin;
    private void Awake()
    {
        _startActive = true;
    }
    private void Start()
    {
        OnWin?.Invoke();
    }

    public void OnLoadWinLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("02_WinScreen");
    }

}
