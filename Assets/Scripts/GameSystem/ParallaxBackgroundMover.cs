using UnityEngine;

public class ParallaxBackgroundMover : MonoBehaviour
{
    public float speed = 10.0f;
    public bool isOpposite = false;

    private float backgroundWidth;
    private float accumulatedX = 0;

    void Start()
    {
        backgroundWidth = GetComponent<Renderer>()?.bounds.size.x ?? 10.0f;
        // backgroundWidth *= transform.localScale.x;
    }

    void Update()
    {
        float frameDelta = speed * (isOpposite ? 1 : -1) * Time.deltaTime;
        float previousAccumulatedX = accumulatedX;
        accumulatedX += frameDelta;
        accumulatedX %= backgroundWidth;
        float deltaX = accumulatedX - previousAccumulatedX;
        float newX = transform.localPosition.x + deltaX;
        transform.localPosition = new(newX, transform.localPosition.y, transform.localPosition.z);
    }
}
