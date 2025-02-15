using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;

    [Space]
    public float maxSP = 100;
    public float currentSP = 0;
    public float skillSP = 25;

    [Space]
    public float damageInvincibleTime = 3f;
    public bool isInvincible = false;
    private float invincibleTimer = 3f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                isInvincible = false;
            }
            else if (spriteRenderer)
            {
                spriteRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 10, 1));
            }
        }
    }


    public void OnBulletHit(int damage)
    {
        DealDamage(damage);
    }

    public void OnGrazing(float deltaTime)
    {
        GainSP(deltaTime * 10f);
    }

    public bool CanUseSkill()
    {
        return currentSP >= skillSP;
    }


    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }

    public void GainSP(float amount)
    {
        currentSP = Mathf.Min(currentSP + amount, maxSP);
    }

    public void DealDamage(int amount)
    {
        if (isInvincible)
        {
            Debug.Log("[PlayerInfo] 무적 상태로 인해 피격되지 않음"); // ✅ 디버깅 추가
            return;
        }

        currentHP = Mathf.Max(currentHP - amount, 0);
        Debug.Log($"[PlayerInfo] 데미지 적용됨! 현재 HP: {currentHP}/{maxHP}"); // ✅ 디버깅 추가

        if (currentHP <= 0)
        {
            Kill();
            return;
        }

        isInvincible = true;
        invincibleTimer = damageInvincibleTime;

        StartCoroutine(InvincibilityFlash());
    }

    public void Kill()
    {
        currentHP = 0;
        gameObject.SetActive(false); // TODO
    }


    // 무적 상태 시 스프라이트 점멸 효과
    private IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // 반투명
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1f); // 원래 색상
            yield return new WaitForSeconds(0.1f);
        }
    }

}