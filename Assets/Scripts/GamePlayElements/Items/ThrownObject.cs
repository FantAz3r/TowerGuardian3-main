using DG.Tweening;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    private float _duration = 1.5f , _damageRadius = 4f, _arcHeight = 3f;
    private int _damage;
    private bool _isFalling = false;


    public void StartFly(int damage, Vector3 endPoint)
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        transform.SetParent(null);
        _damage = damage;
        _collider.enabled = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        Vector3 startPos = transform.position;

        Vector3 peakPos = (startPos + endPoint) * 0.5f + Vector3.up * _arcHeight;

        transform.DOMove(peakPos, _duration * 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _isFalling = true;
            _collider.enabled = true;
            _rigidbody.constraints = RigidbodyConstraints.None;

            transform.DOMove(endPoint, _duration * 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
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
