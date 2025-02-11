using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerInfo playerInfo; // 플레이어 정보 참조
    public Transform player; // 플레이어 Transform 참조

    public Slider hpSlider; // HP 바 (Slider)
    public Slider spSlider; // SP 바 (Slider)

    private Vector3 offset = new Vector3(0, -0.5f, 0); // 하단 위치 조정

    void Start()
    {
        // ✅ HP 및 SP Slider 초기화
        hpSlider.maxValue = playerInfo.maxHP;
        hpSlider.value = playerInfo.currentHP;

        spSlider.maxValue = playerInfo.maxSP;
        spSlider.value = playerInfo.currentSP;
    }

    void LateUpdate()
    {
        // ✅ 플레이어 하단을 따라가도록 설정
        transform.position = player.position + offset;

        // ✅ HP 바 업데이트
        hpSlider.value = playerInfo.currentHP;
        Debug.Log($"[PlayerHUD] HP: {playerInfo.currentHP}/{playerInfo.maxHP}");

        // ✅ SP 바 25 단위 블록으로 업데이트
        float spBlocks = Mathf.Floor(playerInfo.currentSP / 25f) * 25f;
        spSlider.value = spBlocks;
        Debug.Log($"[PlayerHUD] SP: {playerInfo.currentSP}/{playerInfo.maxSP}, SP Blocks: {spBlocks}");
    }
}
