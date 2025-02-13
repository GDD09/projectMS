using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerInfo playerInfo; // 플레이어 정보 참조
    public Transform player; // 플레이어 Transform 참조

    public Slider hpSlider; // HP 바 (Slider)
    public Slider spSlider; // SP 바 (Slider)

    void Start()
    {
        // HP 및 SP Slider 초기화
        hpSlider.maxValue = playerInfo.maxHP;
        hpSlider.value = playerInfo.currentHP;

        spSlider.maxValue = playerInfo.maxSP;
        spSlider.value = playerInfo.currentSP;
    }

    void LateUpdate()
    {
        // HP 바 업데이트
        if (hpSlider.value != playerInfo.currentHP)
        {
            hpSlider.value = Mathf.Lerp(hpSlider.value, playerInfo.currentHP, 0.1f);
        }

        // SP 바 25 단위 블록으로 업데이트
        float spBlocks = Mathf.Floor(playerInfo.currentSP / 25f) * 25f;
        if (spSlider.value != spBlocks)
        {
            spSlider.value = Mathf.Lerp(spSlider.value, spBlocks, 0.1f);
        }
    }
}
