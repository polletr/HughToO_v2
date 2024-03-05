using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class SpikeBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    

    Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 0f;
        _rb.velocity = transform.right * speed;
    }
    private void FixedUpdate()
    {
      
    }

    public void OnDestroy()
    {
        Destroy(gameObject, 0.1f);
    }

    private void OnCollisionEnter2D() => OnDestroy();
}

