using System.Collections;
using UnityEngine;

public class LinearThreewaySpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player; // 플레이어의 위치 참조

    [Header("Triple Pattern Settings")]
    public int linearShots = 3; // 한 번의 패턴에서 발사할 횟수
    public int bulletsPerShot = 5; // 한 번에 발사할 탄 수
    public float shotDelay = 0.3f; // 연속 발사 간격 (탄막 내)
    public float bulletSpacing = 0.5f; // 탄 사이 거리 (발사 방향 기준)
    public float repeatInterval = 2.0f; // 패턴 반복 간격
    public float singleBulletDelay = 0.1f; // 탄 개별 발사 간격
    public float spreadMinAngle = 15f; // 최소 벌어지는 각도
    public float spreadMaxAngle = 30f; // 최대 벌어지는 각도

    void Start()
    {
        StartCoroutine(LoopFirePattern());
    }

    IEnumerator LoopFirePattern()
    {
        while (true) // 무한 반복하여 지속적으로 패턴 발사
        {
            yield return StartCoroutine(FireTriplePattern());
            yield return new WaitForSeconds(repeatInterval); // 패턴 실행 후 일정 시간 대기
        }
    }

    IEnumerator FireTriplePattern()
    {
        if (player == null) yield break; // 플레이어가 없으면 실행하지 않음

        // 첫 번째 발사된 탄의 방향을 저장
        Vector2 centerDirection = (player.position - transform.position).normalized;
        float spreadAngle = Random.Range(spreadMinAngle, spreadMaxAngle); // 15~30도 사이 랜덤
        Vector2 leftDirection = RotateVector(centerDirection, spreadAngle);
        Vector2 rightDirection = RotateVector(centerDirection, -spreadAngle);

        for (int j = 0; j < linearShots; j++) // 패턴 내 반복 발사
        {
            StartCoroutine(FireBullets(centerDirection));
            StartCoroutine(FireBullets(leftDirection));
            StartCoroutine(FireBullets(rightDirection));

            yield return new WaitForSeconds(shotDelay); // 다음 패턴까지 대기
        }
    }

    IEnumerator FireBullets(Vector2 direction)
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 spawnPosition = transform.position; // **오브젝트 중심에서 발사**
            var bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.transform.right = direction; // **첫 번째 탄의 방향을 유지**

            yield return new WaitForSeconds(singleBulletDelay); // 개별 탄 발사 간격 적용
        }
    }

    // 벡터 회전 함수 (각도를 기준으로 회전)
    private Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }
}
