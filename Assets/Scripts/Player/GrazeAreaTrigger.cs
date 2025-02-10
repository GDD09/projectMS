using UnityEngine;

/// <summary>
/// 플레이어의 하위 오브젝트에 부착되어 탄막과의 충돌을 감지하고 Graze 카운터를 쌓는다.
/// </summary>
public class GrazeAreaTrigger : MonoBehaviour
{
    private bool isGrazing = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) { return; }
        isGrazing = true;
    }

    void FixedUpdate()
    {
        if (isGrazing)
        {
            SendMessageUpwards("OnGrazing", Time.fixedDeltaTime);
            isGrazing = false;
        }
    }
}
