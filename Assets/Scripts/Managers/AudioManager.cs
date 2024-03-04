using System.Collections.Generic;
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
     public AudioClipContainer _audioClip;

    public Dictionary<AudioType, AudioClip> AudioClips = new();

    private void Awake()
    {
       /* foreach (AudioPair pair in _audioClip.audioPairs)
        {
            AudioClips.Add(pair.Key, pair.Value);
        }*/
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSpeaker.clip = clip;
             SFXSpeaker.Play();
        }
        else Debug.LogWarning("Audio Clip is null");
    }
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            MusicSpeaker.clip = clip;
            MusicSpeaker.Play();
        }
        //else Debug.LogError("Audio Clip is null");

    }

}
