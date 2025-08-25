using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(CompositeCollider2D))]
public class CompositeColliderDrawer : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    public bool drawFilled = false; // false면 윤곽선만, true면 반투명 채우기

    private CompositeCollider2D comp;

    void OnDrawGizmos()
    {
        if (comp == null)
            comp = GetComponent<CompositeCollider2D>();

        if (comp == null) return;

        Gizmos.color = gizmoColor;

        int pathCount = comp.pathCount;
        Vector2[] points = new Vector2[comp.pointCount];

        for (int i = 0; i < pathCount; i++)
        {
            int pointCount = comp.GetPath(i, points);

            // 경로를 따라 선을 그림
            for (int j = 0; j < pointCount; j++)
            {
                Vector3 start = transform.TransformPoint(points[j]);
                Vector3 end = transform.TransformPoint(points[(j + 1) % pointCount]);
                Gizmos.DrawLine(start, end);
            }

            // 선택적으로 채우기
            if (drawFilled && pointCount >= 3)
            {
                for (int j = 1; j < pointCount - 1; j++)
                {
                    Vector3 v0 = transform.TransformPoint(points[0]);
                    Vector3 v1 = transform.TransformPoint(points[j]);
                    Vector3 v2 = transform.TransformPoint(points[j + 1]);
                    Gizmos.DrawLine(v0, v1);
                    Gizmos.DrawLine(v1, v2);
                    Gizmos.DrawLine(v2, v0);
                }
            }
        }
    }
}
