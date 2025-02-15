using UnityEngine;

/**
* bullet에 flag 설정 후 player / mob 피격 설정
* (초기에는 mob 탄환 -> skill 범위 내에 있으면 player 탄환)
* (필요에 따라 sprite 변경도 필요함 / 지금은 대충 색상 넣은 원으로 진행)
* 피격 계산은 후에 진행
*/

public class Bullet : MonoBehaviour
{
    public bool isPlayerBullet = false; // 초기 상태는 몹 탄환
    public int damage = 10;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }

    void Update()
    {
        // BulletArea를 벗어나면 직접 삭제
        if (!Game.IsInBulletArea(transform))
        {
            Destroy(gameObject);
        }
    }

    void MakePlayerBullet()
    {
        isPlayerBullet = true;
        spriteRenderer.color = Color.blue;  // TODO
    }

    public void ReflectFromPlayer(Transform playerTransform)
    {
        if (isPlayerBullet) { return; }
        MakePlayerBullet();

        // 방향 반사 처리
        Vector3 playerPos = playerTransform.position;
        Vector3 bulletPos = transform.position;
        Vector3 reflectDir = (playerPos - bulletPos).normalized * -1;
        float angle = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 속도 좀 더 빠르게
        SendMessage("MultiplyBulletSpeed", 1.5f);
    }

    public void Hit()
    {
        // 피격 처리
        Destroy(gameObject);
    }
}
