using UnityEngine;

public class Mob : MonoBehaviour
{
    public int damageCount = 0; // 피격 횟수 저장

    void Start()
    {
        damageCount = 0;
    }

    // 피격 메서드 (나중에 구현 예정)
    public virtual void TakeDamage()
    {
        damageCount++;
        Debug.Log($"Mob 피격: {damageCount}번 맞음");
    }
}
