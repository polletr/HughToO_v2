using UnityEngine;
[CreateAssetMenu]
public class AudioClipContainer : ScriptableObject
{
    [Header("BGMusic")]

    public AudioClip MainMenu;
    public AudioClip Cave;
    public AudioClip DeepCave;
    public AudioClip Grassland;

    [Header("WorldSFX")]

    public AudioClip CollectedPickup;
    public AudioClip TeleportOut;
    public AudioClip TeleportIn;
    public AudioClip Teleport;


    [Header("PlayerSFX")]

    public AudioClip Jump;
    public AudioClip Death;
    public AudioClip Glide;
    public AudioClip Drown;
    public AudioClip Hit;

    public AudioClip WaterSplash;
    public AudioClip IcePunch;
    public AudioClip WindStun;



}
