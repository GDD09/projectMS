using System.Collections;
using UnityEngine;

/// <summary>
/// ArcGun은 원호 형태로 Bullet을 발사하는 Gun이다.
/// </summary>
public class ArcGun : GunBase
{
    [Space]
    [Min(1)]
    public int bulletCount = 20;

    [Min(0)]
    public float bulletInterval = 0.03f;
    public float radius = 0f;

    [Range(0, 360)]
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
            float t = (float)i / bulletCount;
            float rot = transform.eulerAngles.z * Mathf.Deg2Rad;
            float maxRad = arcSizeDeg * Mathf.Deg2Rad;
            float rotOffset = -maxRad / 2f;
            float x0 = Mathf.Cos(maxRad * t + rot + rotOffset);
            float y0 = Mathf.Sin(maxRad * t + rot + rotOffset);

            SpawnBullet(transform.position + new Vector3(x0 * radius, y0 * radius, 0.0f), new Vector2(x0, y0));

            if (bulletInterval > 0f)
            {
                yield return new WaitForSeconds(bulletInterval);
            }
        }
    }
}
