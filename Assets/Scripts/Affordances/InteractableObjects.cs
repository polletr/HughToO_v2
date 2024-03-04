using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObjects : MonoBehaviour
{
    public UnityEvent KillObject;
    public UnityEvent InteractObject;

    public enum ObjectType
    {
        Fire,
        BreakableWall,
        Enemy,
        Mushroom,
        Vine
    }

    public ObjectType currentType;

    public void KillObj()
    {
        KillObject?.Invoke();
        PlayKillAudio();
        //Play Animation
        //Play Sound
        //GetComponent<Collider2D>().enabled = false;
    }

    public void InteractObj()
    {
        InteractObject?.Invoke();
        PlayInteractAudio();
        //Play Animation
        //Play Sound
    }
    public void PlayKillAudio()
    {
        switch (currentType)
        {
            case ObjectType.Fire:
                AudioManager.Instance.PlaySFX(AudioManager.Instance._audioClip.FireEstinguish);
                break;
            case ObjectType.BreakableWall:
                AudioManager.Instance.PlaySFX(AudioManager.Instance._audioClip.BreakingWall);
                break;
            case ObjectType.Enemy:
                AudioManager.Instance.PlaySFX(AudioManager.Instance._audioClip.EnemyDeath);
                break;
            default:
                break;
        }
    }
    public void PlayInteractAudio()
    {
        switch (currentType)
        {
            case ObjectType.Mushroom:
                AudioManager.Instance.PlaySFX(AudioManager.Instance._audioClip.PlantWatering);
                break;
            case ObjectType.Vine:
                AudioManager.Instance.PlaySFX(AudioManager.Instance._audioClip.PlantWatering);
                break;
            case ObjectType.Enemy:
                AudioManager.Instance.PlaySFX(AudioManager.Instance._audioClip.EnemyStun);
                break;
            default:
                break;
        }
    }

}
