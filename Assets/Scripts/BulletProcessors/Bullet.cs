using UnityEngine;

    /**
    * bullet에 flag 설정 후 player / mob 피격 설정
    * (초기에는 mob 탄환 -> skill 범위 내에 있으면 player 탄환)
    * (필요에 따라 sprite 변경도 필요함 / 지금은 대충 색상 넣은 원으로 진행)
    */
public class Bullet : MonoBehaviour
{
    public bool isPlayerBullet = false; // 초기 상태는 몹 탄환
    private SpriteRenderer spriteRenderer;
    
    // 당장은 red blue로 진행하고 나중에 스프라이트 이름에 맞게 수정
    public Color mobBulletColor = Color.red;
    public Color playerBulletColor = Color.blue;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateBulletColor();
    }

    private void UpdateBulletColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = isPlayerBullet ? playerBulletColor : mobBulletColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 몹 탄환이 Skill 범위에 닿으면 플레이어 탄환으로 전환
        if (!isPlayerBullet && collision.CompareTag("SkillRange"))
        {
            isPlayerBullet = true;
            UpdateBulletColor();
        }
    }

    // 피격계산 등은 후에 진행
}
