using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = new Color(1f, 0f, 0f, 0.25f);
    public IEnumerable<IDemageable> GetTargets(Transform attackPoint, float range)
    {
        List<IDemageable> targets = new List<IDemageable>();

        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, range);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.GetComponent<Player>() != null)
                continue;

            IDemageable damageable = collider.gameObject.GetComponent<IDemageable>();
            targets.Add(damageable);
        }
        SetAttackData(attackPoint, range);
        return targets;
    }

    private Transform _attackPoint;
    private float _range;

    public void SetAttackData(Transform attackPoint, float range)
    {
        _attackPoint = attackPoint;
        _range = range;
    }

    private void OnDrawGizmos()
    {
        if (_attackPoint == null)
            return;

        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(_attackPoint.position, _range);
    }
}
