using Unity.VisualScripting;
using UnityEngine;

public class LinearMove : MonoBehaviour
{
    public float speed = 10.0f;

    [InspectorRange(0.0f, 360.0f)]
    public float angleDeg = 0.0f;

    [HideInInspector]
    public float angle
    {
        get => angleDeg * Mathf.Deg2Rad;
        set => angleDeg = value * Mathf.Rad2Deg;
    }

    void Update()
    {
        var mainAngle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(mainAngle + angle), Mathf.Sin(mainAngle + angle));
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void MultiplyBulletSpeed(float mul)
    {
        speed *= mul;
    }
}