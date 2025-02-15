using System.Collections;
using UnityEngine;

/// <summary>
/// SphereGun은 원형으로 Bullet을 발사하는 Gun이다.
/// </summary>
public class SphereGun : GunBase
{
    [Space]
    [Min(1)]
    public int bulletCount = 60;
    public float radius = 0f;


    public override void FireBullets()
    {
        if (!isActiveAndEnabled) { return; }

        for (int i = 0; i < bulletCount; i++)
        {
            float t = (float)i / bulletCount;
            float x = Mathf.Cos(2f * Mathf.PI * t) * radius;
            float y = Mathf.Sin(2f * Mathf.PI * t) * radius;
            Vector2 direction = new(Mathf.Cos(2f * Mathf.PI * t), Mathf.Sin(2f * Mathf.PI * t));

            SpawnBullet(transform.position + new Vector3(x, y, 0.0f), direction);
        }
    }
}
