using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static Vector3 CalculateThrowForce(Vector3 start, Vector3 target, float angleDegrees = 45f)
    {
        float g = -Physics.gravity.y;
        float treshold = 0.8f;
        Vector3 dir = target - start;
        float y = dir.y;
        dir.y = 0;

        float x = dir.magnitude * treshold;
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
            int j = UnityEngine.Random.Range(i, n);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        return list;
    }

    public static T SelectAndUpdateWeights<T>(Dictionary<T, float> targetWeights, Dictionary<T, float> startWaights, out Dictionary<T, float> newWeights)
    {
        float totalWeight = targetWeights.Values.Sum();
        float random = UnityEngine.Random.Range(0, totalWeight);

        float sum = 0;
        T chosen = default;

        foreach (var kvp in startWaights)
        {
            sum += kvp.Value;

            if (random <= sum)
            {
                chosen = kvp.Key;
                break;
            }
        }

        float chosenWeight = startWaights[chosen];
        float newChosenWeight = chosenWeight * (chosenWeight / totalWeight);
        float freedWeight = chosenWeight - newChosenWeight;

        var others = startWaights.Keys.Where(k => EqualityComparer<T>.Default.Equals(k, chosen) == false).ToList();
        float sumOthersInitial = others.Sum(k => startWaights[k]);

        newWeights = new Dictionary<T, float>();
        newWeights[chosen] = newChosenWeight;

        foreach (var other in others)
        {
            newWeights[other] = startWaights[other] + freedWeight * (startWaights[other] / sumOthersInitial);
        }

        return chosen;
    }


    public static T SelectByWeights<T>(Dictionary<T, float> targetWeights)
    {
        float totalWeight = 0f;
        float cumulativeWeight = 0f;

        if (targetWeights == null || targetWeights.Count == 0)
            throw new ArgumentException("Словарь пуст или равен null");

        foreach (var weight in targetWeights.Values)
        {
            if (weight < 0)
                throw new ArgumentException("Вес не может быть отрицательным");

            totalWeight += weight;
        }

        if (totalWeight == 0)
            throw new InvalidOperationException("Суммарный вес равен нулю");

        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

        foreach (var pair in targetWeights)
        {
            cumulativeWeight += pair.Value;

            if (randomValue <= cumulativeWeight)
                return pair.Key;
        }

        return targetWeights.Keys.Last();
    }

}
