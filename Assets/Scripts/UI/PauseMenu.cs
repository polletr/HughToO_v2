using UnityEngine;

public class PauseMenu : Menu
{
    private bool _isPaused;
    [SerializeField] private GameObject _SettingsMenu;
    private void Awake()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance._audioClip.BGMusic);
        _startActive = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //add and input manager to handle this later 
        {
            OnTogglePauseMenu();   
        }
    }
    public void OnTogglePauseMenu()
    {
        if (_isPaused)
            AudioManager.Instance.PlayMusic(AudioManager.Instance._audioClip.MainMenu);
        else
            AudioManager.Instance.PlayMusic(AudioManager.Instance._audioClip.BGMusic);


        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        DisableScreens();
        _SettingsMenu.SetActive(false);
        _currentMenu.SetActive(_isPaused);
    }

}
