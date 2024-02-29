using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private AudioClip _teleportOutClip, _teleportInClip;
    [SerializeField] private GameObject _teleportParticlePrefab;
    [SerializeField] private float _teleportDelay = 0.5f;

    [SerializeField] private PlayerBaseInfo PlayerData;
    [SerializeField] private GameObject _blackScreen;

    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            GetComponentInParent<Water>()._teleportPosition = this.transform.position;
        }
    }

    public void CheckPointTeleport(Vector3 _teleportPosition)
    {
        if(_teleportPosition == this.transform.position)
        {
            player.transform.position = _teleportPosition;
            player.ExitPotatoState();
        }
    }

}
