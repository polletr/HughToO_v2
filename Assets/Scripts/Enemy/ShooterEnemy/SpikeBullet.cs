using UnityEngine;

public class SpikeBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float timeToDestorySelf = 10f;


    void Awake()
    {
        Destroy(gameObject, timeToDestorySelf);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D() => Destroy(gameObject, 0.1f);
}

