using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;

    public int maxSP = 100;
    public int currentSP = 0;

    void Start()
    {
        // 초기 상태 설정 (필요하면 이후에 수정 가능)
        currentHP = maxHP;
        currentSP = 0;
    }

    // HP 회복 메서드
    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }

    // SP 증가 메서드
    public void GainSP(int amount)
    {
        currentSP = Mathf.Min(currentSP + amount, maxSP);
    }

    // 테스트용: HP와 SP 정보 출력
    public void PrintStatus()
    {
        Debug.Log($"Player HP: {currentHP}/{maxHP}, SP: {currentSP}/{maxSP}");
    }
}
