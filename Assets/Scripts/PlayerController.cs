using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Speed of the player. [unit/second]
    public float speed = 10.0f;

    // Lock the player movement?
    public bool locked = false;

    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (locked)
        {
            movement = Vector2.zero;
        }
        else
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movement = movement.normalized;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }
}
