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
        // 프레임 관련 처리 없어도 괜찮은건가?
    }

    /**
    * shift 입력시 기존 속도보다 느려지고 피격점 보이도록 추가 필요
    * 아마 Player에도 피격 포인트 component로 추가해야할듯
    */

}