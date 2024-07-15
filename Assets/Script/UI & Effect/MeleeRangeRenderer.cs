using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MeleeRangeRenderer : MonoBehaviour
{
    public GameObject target;
    public int segments = 50;
    public int chargeSegments = 50;
    public float angle = 90f;
    [HideInInspector] public float radius = 5.0f;
    public Vector2 direction = Vector2.right;

    [SerializeField] private LineRenderer projectilePredictLine;
    [SerializeField] public LineRenderer projectileChargeLine;

    private void Start()
    {
        projectilePredictLine.positionCount = segments + 1;
        projectileChargeLine.positionCount = chargeSegments + 1;
        DrawArc();
    }

    [ContextMenu("Create Arc")]
    public void DrawArc()
    {
        Vector3[] points = new Vector3[segments + 1];
        float angleIncrement = angle / segments;
        Vector2 center = target.transform.position;

        direction = target.transform.position - GameManager.Instance.mutation.transform.position;
        float magnitude = direction.magnitude;
        float showRadius = radius*3;
        if (magnitude > showRadius){
            projectilePredictLine.startColor = new Color(projectilePredictLine.startColor.r, projectilePredictLine.startColor.g, projectilePredictLine.startColor.b, 0f);
            projectilePredictLine.endColor = new Color(projectilePredictLine.startColor.r, projectilePredictLine.startColor.g, projectilePredictLine.startColor.b, 0f);
        }
        else
        {
            projectilePredictLine.startColor = new Color(projectilePredictLine.startColor.r, projectilePredictLine.startColor.g, projectilePredictLine.startColor.b, 1f - 1f * (magnitude / showRadius));
            projectilePredictLine.endColor = new Color(projectilePredictLine.startColor.r, projectilePredictLine.startColor.g, projectilePredictLine.startColor.b, 1f - 1f * (magnitude / showRadius));
        }
        float startAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angle / 2 + 180;
        Quaternion rotation = Quaternion.Euler(0, 0, startAngle);

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = angleIncrement * i;
            Quaternion segmentRotation = Quaternion.Euler(0, 0, currentAngle);
            Vector2 point = rotation * segmentRotation * new Vector2(radius, 0);
            points[i] = new Vector3(point.x, point.y, 0) + (Vector3)center;
        }

        projectilePredictLine.SetPositions(points);
    }

    public void DrawChargeArc(float t)
    {
        int currentSegments = Mathf.FloorToInt(chargeSegments * t);
        Vector3[] points = new Vector3[currentSegments + 1];
        float angleIncrement = angle * 1.04f / chargeSegments;
        Vector2 center = target.transform.position;

        float startAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angle * 1.05f / 2 + (180 + angle * 0.02f);
        Quaternion rotation = Quaternion.Euler(0, 0, startAngle);

        for (int i = 0; i <= currentSegments; i++)
        {
            float currentAngle = angleIncrement * i;
            Quaternion segmentRotation = Quaternion.Euler(0, 0, currentAngle);
            Vector2 point = rotation * segmentRotation * new Vector2(radius, 0);
            points[i] = new Vector3(point.x, point.y, 0) + (Vector3)center;
        }

        projectileChargeLine.positionCount = currentSegments + 1;
        projectileChargeLine.SetPositions(points);
    }
}
