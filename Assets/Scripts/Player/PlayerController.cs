using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Speed of the player. [unit/second]
    public float speed = 5.0f;

    // Slower speed of the player. [unit/second]
    public float slowSpeed = 2.5f;

    // Lock the player movement?
    public bool locked = false;


    private Vector2 movement;
    private Rigidbody2D rb;
    private GameObject hitPoint;

    private bool isSlowMode = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitPoint = transform.Find("HitPoint").gameObject;
    }

    void Update()
    {
        if (locked)
        {
            isSlowMode = false;
            movement = Vector2.zero;
        }
        else
        {
            isSlowMode = Input.GetButton("SlowMode");
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movement = movement.normalized;
        }

        hitPoint?.SetActive(isSlowMode);
    }

    void FixedUpdate()
    {
        // RigidBody의 속도값을 직접 조작. 추가적인 DeltaTime 처리는 필요하지 않다.
        var currentSpeed = isSlowMode ? slowSpeed : speed;
        rb.linearVelocity = movement * currentSpeed;
    }
}