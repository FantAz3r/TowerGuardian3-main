using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static Vector3 CalculateThrowForce(Vector3 start, Vector3 target, float angleDegrees = 45f)
    {
        float g = -Physics.gravity.y;
        Vector3 dir = target - start;
        float y = dir.y;
        dir.y = 0;
        float x = dir.magnitude;
        float angle = angleDegrees * Mathf.Deg2Rad;

        if (x < 0.001f) return Vector3.zero;

        float denom = x * Mathf.Tan(angle) + y;
        if (denom <= 0f) return Vector3.zero;

        float vSqr = (g * x * x) / (2f * Mathf.Cos(angle) * Mathf.Cos(angle) * denom);
        if (vSqr <= 0f) return Vector3.zero;

        float v = Mathf.Sqrt(vSqr);
        Vector3 result = dir.normalized * v * Mathf.Cos(angle);
        result.y = v * Mathf.Sin(angle);
        return result;
    }

    public static List<T> GetObjectsSortedByDistance<T>(List<T> objects, Vector3 referencePoint) where T : Component
    {
        if (objects == null || objects.Count == 0)
            return new List<T>();

        return objects
            .Where(obj => obj != null)
            .OrderBy(obj => (obj.transform.position - referencePoint).sqrMagnitude)
            .ToList();
    }

    public static List<T> Shuffle<T>(List<T> list)
    {
        int n = list.Count;

        for (int i = 0; i < n - 1; i++)
        {
            int j = Random.Range(i, n);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        return list;
    }
}
