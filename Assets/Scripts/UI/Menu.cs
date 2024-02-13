using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [Header("Menu Game Objects")]
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _creditsMenu;
    [SerializeField]
    private GameObject _settingsMenu;
    [SerializeField]
    private GameObject _controlsMenu;

    [Header("Audio Souce")]
    [SerializeField]
    private AudioSource _BgMusicSpeaker;
    [SerializeField]
    private AudioSource _SFXSpeaker;
    [SerializeField, Tooltip("Audio Mixer form the Assets folder")]
    private AudioMixer _MasterAudioMixer;

    [Header("Audio Clip Container")]
    [SerializeField] AudioClipContainer Audio;

    [Header("Sliders In Settings")]
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Awake()
    {
        DisableScreens();
        SetUpVolumeValues();
        PlayBGMusic();
    }

    private void Update()
    {
        ListenForVolumeChange();
    }


    #region Menu Actions
    private void PlayBGMusic()
    {
        _BgMusicSpeaker.clip = Audio.BgMusicClip;
        if (_BgMusicSpeaker.clip != null && _BgMusicSpeaker != null)
            _BgMusicSpeaker.Play();
        else
            Debug.LogWarning("BG Audio Clip or Mixer Not assigned to SceneManager");
    }
    private void DisableScreens()
    {
        _mainMenu?.SetActive(true);
        _creditsMenu?.SetActive(false);
        _settingsMenu?.SetActive(false);
        _controlsMenu?.SetActive(false);
    }

    private void SetUpVolumeValues()
    {
        Debug.Log(" Volumes Setup ");
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume",1f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

    }
    private void ListenForVolumeChange()
    {
        MasterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        MusicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        SFXSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }
  
    private void SetMasterVolume()
    {
        float volume = Mathf.Lerp(-80f, 20f, MasterSlider.value);
        _MasterAudioMixer.SetFloat("Master",volume);
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value);
    }
    private void SetMusicVolume()
    {
        float volume = Mathf.Lerp(-80f, 20f, MusicSlider.value);
        _MasterAudioMixer.SetFloat("MusicVolume",volume);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }
    private void SetSFXVolume()
    {

        float volume = Mathf.Lerp(-80f, 20f, SFXSlider.value);
        _MasterAudioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }


    #endregion

    public void OnPlayGame()
    {
        //SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void OnToggleSettings() => _settingsMenu.SetActive(!_settingsMenu.activeSelf);
    public void OnToggleCredits() => _creditsMenu.SetActive(!_creditsMenu.activeSelf);
    public void OnToggleControls() => _controlsMenu.SetActive(!_controlsMenu.activeSelf);


    public void OnQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            CloseTab();
#else
            Application.Quit();
#endif
    }

}
