using System.Collections;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// LinearGun은 직선으로 n개의 Bullet을 발사하는 Gun이다.
/// </summary>
public class LinearGun : GunBase
{
    [Space]
    [Min(1)]
    public int bulletCount = 1;

    [HideIf("IsSingleBullet")]
    [Min(1f / 60f)]
    public float bulletInterval = 0.1f;

    [Range(-180, 180)]
    public float angleOffset = 0f;

    [HideIf("IsSingleBullet")]
    public bool lockOn = false;


    public override void FireBullets()
    {
        if (!isActiveAndEnabled) { return; }

        if (lockOn)
        {
            StartCoroutine(FireBulletsLockOn(Game.PlayerObject.transform.position - transform.position));
        }
        else
        {
            StartCoroutine(FireBulletsCoroutine());
        }
    }

    private bool IsSingleBullet() => bulletCount == 1;

    private IEnumerator FireBulletsCoroutine()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            SpawnBullet(transform.position, angleOffset);
            yield return new WaitForSeconds(bulletInterval);
        }
    }

    private IEnumerator FireBulletsLockOn(Vector2 direction)
    {
        // add the offset to the direction
        direction = Quaternion.Euler(0, 0, angleOffset) * direction;

        for (int i = 0; i < bulletCount; i++)
        {
            SpawnBullet(transform.position, direction);
            yield return new WaitForSeconds(bulletInterval);
        }
    }
}
