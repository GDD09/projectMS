using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;

    [Space]
    public int maxSP = 100;
    public float currentSP = 0;

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
            if (invincibleTimer <= 0) {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                isInvincible = false;
            } else if (spriteRenderer) {
                spriteRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 10, 1));
            }
        }
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

    public void DealDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);

        if (currentHP <= 0)
        {
            Kill();
            return;
        }

        isInvincible = true;
        invincibleTimer = damageInvincibleTime;
    }

    public void Kill()
    {
        currentHP = 0;
        gameObject.SetActive(false);  // TODO
    }
}
