using UnityEngine;

public class Menu : MonoBehaviour
{

    [Header("Menu Game Objects")]
    [SerializeField]
    protected GameObject _currentMenu;
    [SerializeField]
    private GameObject _creditsMenu;
    [SerializeField]
    private GameObject _controlsMenu;

    [SerializeField]
    protected bool _startActive;

    protected bool isMusted;

    private void Awake()
    {
        DisableScreens();
    }

    //Screen Management
    protected void DisableScreens()
    {
        _currentMenu?.SetActive(_startActive);
        _creditsMenu?.SetActive(false);
        _controlsMenu?.SetActive(false);
    }

    //Menu Management

    public void OnToggleCurrentMenu() => _currentMenu.SetActive(!_currentMenu.activeSelf);
    public void OnToggleCredits() => _creditsMenu.SetActive(!_creditsMenu.activeSelf);
    public void OnToggleControls() => _controlsMenu.SetActive(!_controlsMenu.activeSelf);

    public void OnQuitGame()
    {
        /*
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            CloseTab();
#else
            Application.Quit();
#endif
*/    }

}
