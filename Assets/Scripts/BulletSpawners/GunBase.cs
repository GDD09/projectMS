using UnityEngine;

/// <summary>
/// Gun은 Bullet을 발사하는 주체, 즉 총알 스포너를 나타낸다.
/// 
/// Gun을 작동하기 위해서는 FireBullets() 메서드를 호출해야 한다.
/// 이는 GunTriggerer 계열 컴포넌트에서 담당한다.
/// </summary>
public abstract class GunBase : MonoBehaviour
{
    public GameObject bulletPrefab;

    public virtual void FireBullets()
    {
        // override me!
    }


    /// <summary>
    /// Bullet을 발사하는 도우미 메서드.
    /// </summary>
    protected GameObject SpawnBullet(Vector2 position)
    {
        Vector3 pos3 = new(position.x, position.y, transform.position.z);
        return Instantiate(bulletPrefab, pos3, transform.rotation);
    }

    /// <summary>
    /// 방향 벡터를 받아서 Bullet을 발사한다.
    /// </summary>
    protected GameObject SpawnBullet(Vector2 position, Vector2 direction)
    {
        var bullet = SpawnBullet(position);
        bullet.transform.right = new Vector3(direction.x, direction.y, transform.position.z).normalized;
        return bullet;
    }

    /// <summary>
    /// 도 단위의 각도를 받아서 Bullet을 발사한다.
    /// 실제 발사각은 transform의 각도에 따라 달라질 수 있다.
    /// </summary>
    protected GameObject SpawnBullet(Vector2 position, float angleDeg)
    {
        var bullet = SpawnBullet(position);
        bullet.transform.eulerAngles += new Vector3(0.0f, 0.0f, angleDeg);
        return bullet;
    }
}
