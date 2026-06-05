using DG.Tweening;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Items
{
    public class ThrownObject : MonoBehaviour
    {
        private const float Half = 0.5f;

        private CapsuleCollider _collider;
        private Rigidbody _rigidbody;
        private float _duration = 1.3f;
        private float _damageRadius = 4f;
        private float _arcHeight = 4f;
        private int _damage;

        public void StartFly(int damage, Vector3 endPoint)
        {
            _collider = GetComponent<CapsuleCollider>();
            _rigidbody = GetComponent<Rigidbody>();

            transform.SetParent(null);
            _damage = damage;
            _collider.enabled = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

            Vector3 startPos = transform.position;
            Vector3 peakPos = ((startPos + endPoint) * Half) + (Vector3.up * _arcHeight);

            transform.DOMove(peakPos, _duration * Half).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                _collider.enabled = true;
                _rigidbody.constraints = RigidbodyConstraints.None;

                transform.DOMove(endPoint, _duration * Half).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    FallDown();
                });
            });
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
}