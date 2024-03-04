using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private AudioClip _teleportOutClip, _teleportInClip;
    [SerializeField] private GameObject _teleportParticlePrefab;
    [SerializeField] private float _teleportDelay = 1f;

    [SerializeField] private Transform TeleportPos; // empty obj inside teleporter

    [SerializeField] private GameObject currentBackground; // empty obj inside teleporter
    [SerializeField] private GameObject newBackground;

    Player player;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            TransitionManager.Instance.FadeIn();
            StartCoroutine(TP());
            AudioManager.Instance.PlayWorldSFX(AudioManager.Instance._audioClip.TeleportIn);
        }
    }

    private IEnumerator TP()
    {
        yield return new WaitForSeconds(_teleportDelay);
        player.transform.position = TeleportPos.position;
        currentBackground.SetActive(false);
        newBackground.SetActive(true);
        TransitionManager.Instance.FadeOut();
        AudioManager.Instance.PlayWorldSFX(AudioManager.Instance._audioClip.TeleportOut);
    }

}
