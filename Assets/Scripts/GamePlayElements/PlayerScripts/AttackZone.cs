using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = new Color(1f, 0f, 0f, 0.25f);
    private float _range;
    private IDemageable _selfHealth;

    private void Awake()
    {
        _selfHealth = GetComponentInParent<IDemageable>();
    }

    public IEnumerable<IDemageable> GetTargets(float range)
    {
        List<IDemageable> targets = new List<IDemageable>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.GetComponent<IDemageable>() == _selfHealth)
                continue;

            IDemageable damageable = collider.gameObject.GetComponent<IDemageable>();
            targets.Add(damageable);
        }

        SetAttackData(range);

        return targets;
    }

    public void SetAttackData(float range)
    {
        _range = range;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, _range);
    }
}
