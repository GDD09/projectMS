using System.Collections;
using UnityEngine;

/// <summary>
/// SphereGun은 원형으로 Bullet을 발사하는 Gun이다.
/// </summary>
public class SphereGun : GunBase
{
    [Space]
    public int bulletCount = 60;
    public float radius = 0.5f;


    public override void FireBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float t = (float)i / bulletCount;
            float x = Mathf.Cos(2f * Mathf.PI * t) * radius;
            float y = Mathf.Sin(2f * Mathf.PI * t) * radius;
            float angleDeg = t * 360.0f;

            SpawnBullet(transform.position + new Vector3(x, y, 0.0f), angleDeg);
        }
    }
}
