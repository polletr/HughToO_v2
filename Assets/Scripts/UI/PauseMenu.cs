using UnityEngine;

public class PauseMenu : Menu
{
    private bool _isPaused;

    private void Awake()
    {
        _startActive = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //add and input manager to handle this later 
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1;
            DisableScreens();
            _currentMenu.SetActive(_isPaused);
           
        }
    }
}
