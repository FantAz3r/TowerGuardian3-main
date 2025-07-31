using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = new Color(1f, 0f, 0f, 0.25f);
    private Vector3 _attackPoint;
    private float _range;
    private IDemageable _selfHealth;

    private void Awake()
    {
        _selfHealth = GetComponentInParent<IDemageable>();
    }

    public IEnumerable<IDemageable> GetTargets(Vector3 attackPoint, float range)
    {
        List<IDemageable> targets = new List<IDemageable>();

        Collider[] hitColliders = Physics.OverlapSphere(attackPoint, range);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.GetComponent<IDemageable>() == _selfHealth)
                continue;

            IDemageable damageable = collider.gameObject.GetComponent<IDemageable>();
            targets.Add(damageable);
        }

        SetAttackData(attackPoint, range);
        return targets;
    }

    public void SetAttackData(Vector3 attackPoint, float range)
    {
        _attackPoint = attackPoint;
        _range = range;
    }

    private void OnDrawGizmos()
    {
        if (_attackPoint == null)
            return;

        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(_attackPoint, _range);
    }
}
