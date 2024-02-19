using UnityEngine;

public class SpikeBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float timeToDestorySelf = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, timeToDestorySelf);
    }
    private void FixedUpdate()
    {
       // transform.position  * speed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth player))
        {
            //take damage from player
        }
    }
}

