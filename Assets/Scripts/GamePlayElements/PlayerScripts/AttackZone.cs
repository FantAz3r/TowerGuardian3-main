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

    public IEnumerable<Health> GetTargets(float range)
    {
        SetAttackData(range);
        List<Health> targets = new List<Health>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.GetComponent<IDemageable>() == _selfHealth)
                continue;

            Health damageable = collider.gameObject.GetComponent<Health>();

            if(damageable == null)
                continue;

            targets.Add(damageable);
        }

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
