using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UseSkill 메시지를 받았을 때 콜라이더 내의 모든 탄환을 반사한다.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SkillAreaTrigger : MonoBehaviour
{
    private HashSet<Collider2D> colliders = new();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            colliders.Add(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            colliders.Remove(collision);
            colliders.RemoveWhere(c => c is null);
        }
    }


    void UseSkill()
    {
        Debug.Log("스킬 사용!");
        foreach (var collider in colliders)
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            if (bullet == null) { continue; }

            bullet.ReflectFromPlayer(transform);
        }
    }
}
