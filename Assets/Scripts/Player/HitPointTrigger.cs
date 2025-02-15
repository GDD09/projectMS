using UnityEngine;

/// <summary>
/// 플레이어 또는 몹의 HitPointTrigger 오브젝트에 부착되어
/// 탄막과의 충돌을 감지하고 피격 처리를 담당한다.
/// </summary>
public class HitPointTrigger : MonoBehaviour
{
    public bool isPlayer = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) { return; }

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet == null) { return; }

        if (bullet.isPlayerBullet != isPlayer)
        {
            // 총알이 맞았음을 알려준다.
            bullet.Hit();

            // 자신의 피격 처리를 진행한다.
            SendMessageUpwards("OnBulletHit", bullet.damage);
        }

    }
}
