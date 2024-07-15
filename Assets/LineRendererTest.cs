using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererTest : MonoBehaviour
{
    public GameObject target; 
    public int segments = 50; 
    public float radius = 5.0f; 
    public float angle = 90f;
    public Vector2 direction = Vector2.right;

    [SerializeField] private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        // lineRenderer.positionCount = segments + 1;
        CreateArc();
    }
    [ContextMenu("Create Arc")]
    void CreateArc()
    {
       Vector3[] points = new Vector3[segments + 1];
        float angleIncrement = angle / segments;
        Vector2 center = target.transform.position;

        // Tính toán điểm đầu của cung tròn dựa trên hướng cụ thể
        float startAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angle /2;
        Quaternion rotation = Quaternion.Euler(0, 0, startAngle);

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = angleIncrement * i;
            Quaternion segmentRotation = Quaternion.Euler(0, 0, currentAngle);
            Vector2 point = rotation * segmentRotation * new Vector2(radius, 0);
            points[i] = new Vector3(point.x, point.y, 0) + (Vector3)center;
        }

        lineRenderer.SetPositions(points);
    }
}
