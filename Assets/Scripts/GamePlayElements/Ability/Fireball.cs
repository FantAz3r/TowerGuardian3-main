using DG.Tweening;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private LayerMask _damageableLayers;

    private FireballConfig _config;
    private Vector3 _endPoint;
    private Collider _collider;
    private ISpawnerService _spawnerService;

    private void Awake()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        _collider = GetComponent<Collider>();
    }

    public void Init(Vector3 endPoint, FireballConfig config)
    {
        _config = config;
        _endPoint = endPoint;
        Fly();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & _collisionLayers) == 0) return;

        Explode();
    }

    private void Fly()
    {
        transform.parent = null;

        if (_collider != null) _collider.enabled = true;

        float distance = Vector3.Distance(transform.position, _endPoint);
        float duration = distance / _config.FlySpeed;

        transform.DOMove(_endPoint, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }


    private void Explode()
    {
        _spawnerService.SendEffectReqest(EffectType.Expload, transform.position);
        ApplyDamage();
        if (_collider != null) _collider.enabled = false;
        gameObject.SetActive(false);
    }

    private void ApplyDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _config.ExploadRange, _damageableLayers);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDemageable>(out var damageable))
            {
                damageable.TakeDamage(_config.ExploadDamage);
            }
        }
    }
}
