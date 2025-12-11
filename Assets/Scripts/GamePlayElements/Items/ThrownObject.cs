using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    private float _damageRadius = 4f;
    private int _damage;
    private float _hitTriggerRange = 2;

    public void StartFly(int damage)
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        _damage = damage;
        _collider.radius = _hitTriggerRange;
        _collider.height = _hitTriggerRange;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_rigidbody.velocity.y < 0)
        {
            FallDown();
        }
    }

    public void FallDown()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _damageRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Health>(out var item))
            {
                item.TakeDamage(_damage);
            }
        }

        Destroy(gameObject);
    }
}
