using UnityEngine;

public static class Util2D
{
    public static class Gizmos
    {
        public static void DrawArrow(Vector2 from, Vector2 to)
        {
            UnityEngine.Gizmos.DrawLine(from, to);

            var direction = (to - from).normalized;
            var leftWing = (Vector2)(Quaternion.Euler(0, 0, 45) * direction * -0.25f);
            var rightWing = (Vector2)(Quaternion.Euler(0, 0, -45) * direction * -0.25f);

            UnityEngine.Gizmos.DrawLine(to, to + leftWing);
            UnityEngine.Gizmos.DrawLine(to, to + rightWing);
        }

        public static void DrawRect(Rect rect)
        {
            UnityEngine.Gizmos.DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y));
            UnityEngine.Gizmos.DrawLine(new Vector2(rect.x + rect.width, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height));
            UnityEngine.Gizmos.DrawLine(new Vector2(rect.x + rect.width, rect.y + rect.height), new Vector2(rect.x, rect.y + rect.height));
            UnityEngine.Gizmos.DrawLine(new Vector2(rect.x, rect.y + rect.height), new Vector2(rect.x, rect.y));
        }
    }

    // Extension Methods //

    public static void SetPos(this Transform transform, Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public static Vector2 GetPos(this Transform transform)
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public static void SetDeg(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public static void SetRad(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
    }

    public static void GetDeg(this Transform transform, out float angle)
    {
        angle = transform.rotation.eulerAngles.z;
    }

    public static void GetRad(this Transform transform, out float angle)
    {
        angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
    }

    public static void Translate(this Transform transform, Vector2 translation)
    {
        transform.position = new Vector3(transform.position.x + translation.x, transform.position.y + translation.y, transform.position.z);
    }

    public static void MoveTowards(this Transform transform, Vector2 target, float delta)
    {
        var newPos = Vector2.MoveTowards(transform.position, target, delta);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    public static void LookAt(this Transform transform, Vector2 target)
    {
        var direction = target - (Vector2)transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}