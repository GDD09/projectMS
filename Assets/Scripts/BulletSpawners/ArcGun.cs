using System.Collections;
using UnityEngine;

/// <summary>
/// ArcGun은 원호 형태로 Bullet을 발사하는 Gun이다.
/// </summary>
public class ArcGun : GunBase
{
    [Space]
    public int bulletCount = 20;
    public float bulletInterval = 0.03f;
    public float radius = 0f;
    public float arcSizeDeg = 60f;


    public override void FireBullets()
    {
        if (!isActiveAndEnabled) { return; }
        StartCoroutine(FireBulletsCoroutine());
    }

    private IEnumerator FireBulletsCoroutine()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float rot = transform.eulerAngles.z * Mathf.Deg2Rad;
            float t = (float)i / bulletCount;
            float x0 = Mathf.Cos(2f * Mathf.PI * t + rot);
            float y0 = Mathf.Sin(2f * Mathf.PI * t + rot);

            float angleOffset = -arcSizeDeg * bulletCount / 2f;
            float angle = arcSizeDeg * t + angleOffset;

            SpawnBullet(transform.position + new Vector3(x, y, 0.0f), angle);

            if (bulletInterval > 0f)
            {
                yield return new WaitForSeconds(bulletInterval);
            }
        }
    }
}
