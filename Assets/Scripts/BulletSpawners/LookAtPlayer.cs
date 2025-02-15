using UnityEngine;

/// <summary>
/// 플레이어를 바라본다.
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
    void Update()
    {
        if (Game.PlayerObject == null) { return; }

        var position = Game.PlayerObject.transform.position;
        var direction = position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 참 쉽죠?
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
