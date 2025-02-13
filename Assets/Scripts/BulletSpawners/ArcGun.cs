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
    public float radius = 0.5f;
    public float startAngleDeg = 0f;
    public float arcSizeDeg = 60f;


    public override void FireBullets()
    {
        StartCoroutine(FireBulletsCoroutine());
    }

    private IEnumerator FireBulletsCoroutine()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float t = (float)i / bulletCount;
            float x = Mathf.Cos(2f * Mathf.PI * t) * radius;
            float y = Mathf.Sin(2f * Mathf.PI * t) * radius;
            float angle = startAngleDeg + arcSizeDeg * t;

            SpawnBullet(transform.position + new Vector3(x, y, 0.0f), angle);

            if (bulletInterval > 0f)
            {
                yield return new WaitForSeconds(bulletInterval);
            }
        }
    }
}
