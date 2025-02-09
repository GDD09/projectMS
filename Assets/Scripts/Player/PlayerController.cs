using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
// Rigbody 옮김
public class PlayerController : MonoBehaviour
{
    // Speed of the player. [unit/second]
    public float speed = 5.0f;

    // Slower speed of the player. [unit/second]
    public float slowSpeed = 2.5f;

    // Lock the player movement?
    public bool locked = false;
    


    private Vector2 movement;
    private GameObject hitPoint;


    private bool isSlowMode = false;


    void Start()
    {
        hitPoint = transform.Find("HitPoint").gameObject;

        if (hitPoint != null)
        {
            Debug.LogError("HitPoint 오브젝트를 찾을 수 없습니다!");
        }
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

        if (hitPoint != null)
        {
            // 🔹 HitPoint는 항상 활성화 (충돌 감지 유지), 대신 Sprite만 활성화/비활성화
            hitPoint.GetComponent<SpriteRenderer>().enabled = isSlowMode;
        }
    }

    void FixedUpdate()
    {
        // RigidBody의 속도값을 직접 조작. 추가적인 DeltaTime 처리는 필요하지 않다.
        var currentSpeed = isSlowMode ? slowSpeed : speed;
        // rb HitPoint로 옮겨서 직접 transform으로 계산
        // rb.linearVelocity = movement * currentSpeed;
        transform.position += (Vector3)(movement * currentSpeed * Time.fixedDeltaTime);
        

        if (hitPoint != null)
        {
            hitPoint.transform.position = transform.position;
        }
    }
}