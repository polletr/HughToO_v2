using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class SpikeBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float timeToDestorySelf = 10f;

    Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 0f;
        //Destroy(gameObject, timeToDestorySelf);
    }
    private void FixedUpdate()
    {
        _rb.velocity = -Vector3.right * speed;
    }
    private void OnCollisionEnter2D() => Destroy(gameObject, 0.1f);
}

