using System.Collections.Generic;
using System.Linq;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using UnityEngine;
using UnityEngine.AI;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies
{
    public class ThrownObjectDetector : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 20f;
        [SerializeField] private LayerMask _resourceItemLayer;

        public Transform GetNearestResource()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _detectionRadius, _resourceItemLayer);
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

            ResourceItem nearest = Utils.Utils.GetObjectsSortedByDistance(items, transform.position).First();
            NavMeshObstacle obstacle = nearest.GetComponent<NavMeshObstacle>();
            obstacle.enabled = false;
            return nearest.transform;
        }
    }
}
