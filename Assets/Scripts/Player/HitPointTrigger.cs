using UnityEngine;

/*
이게 로직 자체는 정상적으로 넣은 것 같은데
충돌판정이 계속 Player 자체로 판정을 진행함

의도는 Player안에 HitPoint 오브젝트 기준으로 피격판정, graze판정, 스킬판정을 주고싶음

프리팹은 변경사항 크게 없으니 스테이지에 올려놓은 객체 위주로 확인해볼것것
*/

public class HitPointTrigger : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private float lastGrazeTime;
    public float grazeCooldown = 0.1f; // Graze 간격 조절

    void Start()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();

        if (playerInfo == null)
        {
            Debug.LogError("[HitPointTrigger] PlayerInfo를 찾을 수 없습니다! HitPoint가 Player의 자식인지 확인하세요.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[HitPointTrigger] 충돌 감지: {collision.gameObject.name}");

        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null && !bullet.isPlayerBullet)
        {
            Debug.Log("[HitPointTrigger] 적 탄막 충돌 감지!");

            if (playerInfo != null && !playerInfo.isInvincible)
            {
                playerInfo.DealDamage(1);
                Debug.Log($"[HitPointTrigger] 플레이어 피격! 현재 HP: {playerInfo.currentHP}/{playerInfo.maxHP}");
            }

            Destroy(collision.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null && !bullet.isPlayerBullet)
        {
            if (Time.time - lastGrazeTime >= grazeCooldown)
            {
                Debug.Log("[HitPointTrigger] Graze 발생!");

                playerInfo.GainSP(1);
                lastGrazeTime = Time.time;
            }
        }
    }
}
