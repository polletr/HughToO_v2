using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField]
    private float windForce;

    [SerializeField]
    private Direction windDirection;

    private SpriteRenderer sprite;

    private Vector2 forceDirection;

    private enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        forceDirection = Vector2.zero;


        switch (windDirection)
        {
            case Direction.Right:
                forceDirection = Vector2.right;
                sprite.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                break;
            case Direction.Left:
                forceDirection = Vector2.left;
                sprite.flipX = true;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                break;
            case Direction.Up:
                forceDirection = Vector2.up;
                sprite.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);

                break;
            case Direction.Down:
                forceDirection = Vector2.down;
                sprite.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);

                break;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player.currentStats.currentForm == ScriptableStats.Form.Gas)
            {
                if (windDirection == Direction.Right || windDirection == Direction.Left)
                {
                    //other.GetComponent<Rigidbody2D>().AddForce(forceDirection * windForce * Time.deltaTime);

                    Vector2 velocity = new Vector2(forceDirection.x * windForce, player._rb.velocity.y);
                    player._rb.velocity = velocity;

                }
                else
                {
                    Vector2 velocity = new Vector2(player._rb.velocity.x, forceDirection.y * windForce);
                    player._rb.velocity = velocity;

                }

            }
        }
        // Set the force direction based on the selected enum value

    }
}
