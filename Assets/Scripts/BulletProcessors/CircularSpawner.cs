using System.Collections;
using UnityEngine;

public class CircularSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletCount = 10;
    public float radius = 1.0f;

    public float armDelay = 3.0f;
    public float fireDelay = 0.05f;
    public float elapsedTime = 0.0f;

    private int currentBulletCount = 0;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= armDelay)
        {
            elapsedTime = -fireDelay * currentBulletCount;
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.position += new Vector3(Mathf.Cos(2f * Mathf.PI * i / bulletCount) * radius, Mathf.Sin(2f * Mathf.PI * i / bulletCount) * radius, 0.0f);
            bullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, 360.0f * i / bulletCount);
            currentBulletCount++;
            yield return new WaitForSeconds(fireDelay);
        }
    }
}
