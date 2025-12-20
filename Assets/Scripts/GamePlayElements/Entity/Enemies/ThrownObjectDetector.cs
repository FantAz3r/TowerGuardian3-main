using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrownObjectDetector : MonoBehaviour
{
    public float detectionRadius = 20f;
    public LayerMask resourceItemLayer;

    public Transform GetNearestResource()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, resourceItemLayer);
        List<ResourceItem> items = new List<ResourceItem>();

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<ResourceItem>(out var item))
            {
                items.Add(item);
            }
        }

        if (items.Count == 0)
            return null;

        ResourceItem nearest = items.OrderBy(item => Vector3.Distance(transform.position, item.transform.position)).First();
        Debug.Log($"{nearest} {nearest.transform.position}");
        return nearest.transform;
    }
}
