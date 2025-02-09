using System.Collections;
using UnityEngine;

public class LinearOnewaySpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player; // 플레이어의 위치 참조

    [Header("Linear Pattern Settings")]
    public int linearShots = 3; // 한 번의 패턴에서 발사할 횟수
    public int bulletsPerShot = 5; // 한 번에 발사할 탄 수
    public float shotDelay = 0.3f; // 연속 발사 간격 (탄막 내)
    public float bulletSpacing = 0.5f; // 탄 사이 거리 (발사 방향 기준)
    public float repeatInterval = 2.0f; // 패턴 반복 간격
    public float singleBulletDelay = 0.1f; // 탄 개별 발사 간격

    void Start()
    {
        StartCoroutine(LoopFirePattern());
    }

    IEnumerator LoopFirePattern()
    {
        while (true) // 무한 반복하여 지속적으로 패턴 발사
        {
            yield return StartCoroutine(FireLinearPattern());
            yield return new WaitForSeconds(repeatInterval); // 패턴 실행 후 일정 시간 대기
        }
    }

    IEnumerator FireLinearPattern()
    {
        if (player == null) yield break; // 플레이어가 없으면 실행하지 않음

        // 첫 번째 탄이 발사될 때의 방향을 저장
        Vector2 direction = (player.position - transform.position).normalized;
        Vector3 startPosition = transform.position; // 발사 시작 위치

        for (int j = 0; j < linearShots; j++) // 패턴 내 반복 발사
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Vector3 spawnPosition = startPosition + (Vector3)direction * i * bulletSpacing;
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.right = direction; // **첫 번째 탄의 방향을 유지**
                
                yield return new WaitForSeconds(singleBulletDelay); // 개별 탄 발사 간격 적용
            }

            yield return new WaitForSeconds(shotDelay); // 다음 5발 발사까지 대기
        }
    }
}
