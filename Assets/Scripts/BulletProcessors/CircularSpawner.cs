using System;
using System.Collections;
using UnityEngine;

public class CircularSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;

    [Header("Timing")]
    public float armDelay = 3.0f;
    public float shotDelay = 0.05f;
    public float elapsedTime = 0.0f;

    [Header("Shape")]
    public int bulletCount = 60;
    public int bulletPerShot = 1;
    public float radius = 0.5f;
    public float startAngleDeg = 0.0f;
    public float arcSizeDeg = 360.0f;

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime >= armDelay)
        {
            elapsedTime %= armDelay;
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float t = (float) i / bulletCount;
            float x = Mathf.Cos(2f * Mathf.PI * t) * radius;
            float y = Mathf.Sin(2f * Mathf.PI * t) * radius;
            float angle = startAngleDeg + arcSizeDeg * t;

            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.position += new Vector3(x, y, 0.0f);
            bullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);

            if ((shotDelay > 0f) && (i % bulletPerShot == 0))
            {
                float waitedTime = 0f;
                while (waitedTime < shotDelay)
                {
                    waitedTime += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }
}
