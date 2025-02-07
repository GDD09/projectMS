using UnityEngine;

public class HitPointTrigger : MonoBehaviour
{
    /**
    * hitpoint 기준으로 피격 및 graze 되도록 설정해야함
    * 이 상태가 Player 기준으로 피격되는 상태
    * 아마 rigidbody를 Player가 아닌 Hitpoint로 옮겨야 할듯?
    **/


    private PlayerInfo playerInfo;

    void Start()
    {
        playerInfo = GetComponentInParent<PlayerInfo>(); // 부모(Player)에서 PlayerInfo 가져오기

        if (playerInfo == null)
        {
            Debug.LogError("PlayerInfo를 찾을 수 없습니다!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null && !bullet.isPlayerBullet) // 🔹 적 탄막만 감지
        {
            playerInfo.currentHP--; // HP 감소
            Debug.Log($"플레이어 피격! 현재 HP: {playerInfo.currentHP}/{playerInfo.maxHP}");

            if (playerInfo.currentHP <= 0)
            {
                Debug.Log("플레이어 사망!");
                Destroy(playerInfo.gameObject); // Player 오브젝트 삭제
            }

            Destroy(collision.gameObject); // 탄막 제거
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null && !bullet.isPlayerBullet) // 🔹 적 탄막만 Graze 처리
        {
            playerInfo.GainSP(1); // SP 회복
            Debug.Log($"Graze 발생! 현재 SP: {playerInfo.currentSP}/{playerInfo.maxSP}");
        }
    }
}