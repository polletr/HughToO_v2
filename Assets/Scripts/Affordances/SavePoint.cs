using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class SavePoint : MonoBehaviour
{
    [SerializeField] private AudioClip _teleportOutClip, _teleportInClip;
    [SerializeField] private GameObject _teleportParticlePrefab;
   // [SerializeField] private float _teleportDelay = 0.5f;

    [SerializeField] private PlayerBaseInfo PlayerData;
    [SerializeField] private GameObject _blackScreen;

    private Animator anim;

    private Vector3 _teleportPosition;

    Player player;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        anim = GetComponent<Animator>();
    }
    [SerializeField]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("GrowTree");
            player = other.GetComponent<Player>();
            player.playerData.Data.position[0] = transform.position.x;
            player.playerData.Data.position[1] = transform.position.y;
            player.playerData.Data.position[2] = transform.position.z;

            SaveManager.Instance.SavePlayerData();
        }
    }

}
