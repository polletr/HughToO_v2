using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    [SerializeField]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            //Player particles and sound

           // other.GetComponent<PlayerHealth>().HealToFull();
            Debug.Log("Checkpoint reached");
            Player player = other.GetComponent<Player>();
            player.playerData.Data.position[0] = transform.position.x;
            player.playerData.Data.position[1] = transform.position.y;
            player.playerData.Data.position[2] = transform.position.z;

            SaveManager.Instance.SavePlayerData();
        }
    }

}
