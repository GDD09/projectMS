using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// TimedTriggerer는 일정 시간 간격으로 Gun을 발동하는 컴포넌트이다.
/// </summary>
public class TimedTriggerer : MonoBehaviour
{
    public float interval = 1.0f;

    public bool infinite = true;

    [Min(1)]
    [HideIf("infinite")]
    public int repeatCount = 1;

    [Space]
    public float elapsedTime = 0.0f;

    private int counter = 0;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= interval)
        {
            elapsedTime %= interval;
            counter++;

            SendMessage("FireBullets");

            if (!infinite && counter >= repeatCount)
            {
                enabled = false;
            }
        }
    }

    void OnEnable()
    {
        elapsedTime = 0.0f;
        counter = 0;
    }
}
