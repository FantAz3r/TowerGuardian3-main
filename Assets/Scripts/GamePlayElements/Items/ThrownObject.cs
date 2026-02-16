using DG.Tweening;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    private float _damageRadius = 4f;
    private int _damage;
    private float _duration = 1.5f;
    private bool _isFalling = false;

    private float _arcHeight = 5f;

    public void StartFly(int damage, Transform endPoint)
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        transform.SetParent(null);
        _damage = damage;
        _collider.enabled = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        Vector3 startPos = transform.position;
        Vector3 endPos = endPoint.position;

        Vector3 peakPos = (startPos + endPos) * 0.5f + Vector3.up * _arcHeight;

        transform.DOMove(peakPos, _duration * 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _isFalling = true;
            _collider.enabled = true;
            _rigidbody.constraints = RigidbodyConstraints.None;

            transform.DOMove(endPos, _duration * 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                FallDown();
            });
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isFalling)
            FallDown();
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
