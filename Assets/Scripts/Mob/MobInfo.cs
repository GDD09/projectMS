using UnityEngine;

public class MobInfo : MonoBehaviour
{
    public int damageCount = 0; // 피격 횟수 저장
    public int damageBeforeDeath = 1; // 사망 전까지의 피격 횟수

    void Start()
    {
        damageCount = 0;
    }

    // 피격 메서드 (나중에 구현 예정)
    public virtual void TakeDamage()
    {
        damageCount++;
        Debug.Log($"Mob 피격: {damageCount}번 맞음");

        if (damageCount >= damageBeforeDeath)
        {
            Debug.Log("Mob 사망");
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void OnBulletHit(int _damage)
    {
        TakeDamage();
    }
}
