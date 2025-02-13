using UnityEditor;
using UnityEngine;

/// <summary>
/// Game.BulletArea와 Game.PlayArea를 설정하는 컴포넌트. <br/><br/>
/// 
/// PlayArea는 플레이어의 이동 범위를 제한하는 영역이다. 에디터에서는 초록색으로 표시된다. <br/>
/// BulletArea는 PlayArea보다 약간 크며, Bullet이 소멸되는 영역이다. 에디터에서는 빨간색으로 표시된다. <br/>
/// </summary>
public class PlayAreaIndicator : MonoBehaviour
{
    public float bulletAreaExpand = 0.5f;
    public Rect playArea = new(-10, -10, 20, 20);
    public Rect BulletArea => ExpandRect(playArea, bulletAreaExpand);

    void OnDrawGizmos()
    {
        var offset = (Vector2)transform.position;

        // Game.PlayArea
        Gizmos.color = Color.green;
        GizmoDrawBox(offset, playArea);

        // Game.BulletArea
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        GizmoDrawBox(offset, BulletArea);
    }

    void Awake() => UpdateArea();
    void Update() => UpdateArea();


    private void UpdateArea()
    {
        var offsetedPlayArea = playArea;
        var offsetedBulletArea = BulletArea;
        offsetedPlayArea.position += (Vector2)transform.position;
        offsetedBulletArea.position += (Vector2)transform.position;

        Game.SetPlayArea(offsetedPlayArea, offsetedBulletArea);
    }

    private void GizmoDrawBox(Vector2 offset, Rect rect)
    {
        Gizmos.DrawLineList(new Vector3[]
        {
            offset + rect.min,
            offset + rect.min + new Vector2(rect.width, 0),
            offset + rect.min + new Vector2(rect.width, 0),
            offset + rect.max,
            offset + rect.max,
            offset + rect.min + new Vector2(0, rect.height),
            offset + rect.min + new Vector2(0, rect.height),
            offset + rect.min,
        });
    }

    private Rect ExpandRect(Rect rect, float expand)
    {
        return new Rect(rect.x - expand, rect.y - expand, rect.width + expand * 2f, rect.height + expand * 2f);
    }

}
