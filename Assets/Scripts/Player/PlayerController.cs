using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("플레이어의 이동 속도. [unit/second]")]
    public float speed = 5.0f;

    [Tooltip("슬로우모드 시의 이동 속도. [unit/second]")]
    public float slowSpeed = 2.5f;

    [Tooltip("플레이어의 이동을 잠근다.")]
    public bool locked = false;


    private GameObject hitPointSprite;
    private PlayerInfo playerInfo;
    private Vector2 movement;
    private bool isSlowMode = false;


    void Start()
    {
        hitPointSprite = transform.Find("HitPointSprite").gameObject;
        Debug.Assert(hitPointSprite != null, "HitPointSprite 오브젝트가 있어야 합니다!");

        playerInfo = GetComponent<PlayerInfo>();
        Debug.Assert(playerInfo != null, "PlayerInfo 컴포넌트가 있어야 합니다!");
    }

    void Update()
    {
        if (locked)
        {
            isSlowMode = false;
            movement = Vector2.zero;
        }

        // 슬로우모드 시 HitPointSprite를 활성화한다.
        hitPointSprite?.SetActive(isSlowMode);

        // 플레이어 이동
        float currentSpeed = isSlowMode ? slowSpeed : speed;
        Vector2 currentMovement = movement * currentSpeed * Time.deltaTime;
        transform.Translate(currentMovement);

        // 플레이어가 화면 밖으로 나가지 않도록 제한
        Rect playArea = Game.PlayArea;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, playArea.xMin, playArea.xMax),
            Mathf.Clamp(transform.position.y, playArea.yMin, playArea.yMax),
            transform.position.z
        );
    }


    // PlayerInput 컴포넌트에서 호출되는 이벤트 핸들러
    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>().normalized;
    }

    void OnSlowMode(InputValue value)
    {
        isSlowMode = value.isPressed;
    }

    void OnUseSkill()
    {
        if (locked) { return; }
        if (playerInfo.CanUseSkill())
        {
            playerInfo.GainSP(-playerInfo.skillSP);
            BroadcastMessage("UseSkill");  // TODO: 이게 과연 최선일까?
        }
    }
}