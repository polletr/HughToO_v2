using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Souce")]
    [SerializeField]
    private AudioSource MusicSpeaker;
    [SerializeField]
    private AudioSource SFXSpeaker;

    [Header("Audio Mixer")]
    [SerializeField, Tooltip("Audio Mixer form the Assets folder")]
    private AudioMixer _MasterAudioMixer;

    [Header("Audio Clip Container")]
    [SerializeField] AudioClipContainer Audio;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {

    }
    public void Play(AudioClip clip)
    {
        SFXSpeaker.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        MusicSpeaker.clip = clip;
        MusicSpeaker.Play();
    }

}
