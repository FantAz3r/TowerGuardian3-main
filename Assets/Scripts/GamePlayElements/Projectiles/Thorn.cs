using DG.Tweening;
using UnityEngine;

public class Thorn : Projectile
{
    [SerializeField] private float _appearanceDuration = 1.0f;
    [SerializeField] private AudioClip _clip;

    private ISpawnerService _spawnerService;
    private float _damage = 10f;
    private Vector3 _scale = new Vector3(2, 2, 2);
    private bool _canDamage = true;
    private Collider _collider;

    private void Awake()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        _collider = GetComponent<Collider>();
        _collider.enabled = true;
    }

    public void Init(int damage)
    {
        _damage = damage;
    }

    public override void Appear()
    {
        Vector3 startPosition = transform.position - Vector3.up;
        transform.position = startPosition;
        transform.localScale = Vector3.zero;

        _spawnerService.SendSoundReqest(_clip, transform.position);

        transform.DOScale(_scale, _appearanceDuration);
        transform.DOMove(startPosition + Vector3.up * 2, _appearanceDuration)
            .OnComplete(() =>
            {
                _canDamage = false;
            });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canDamage && collision.collider.TryGetComponent(out PlayerHealth health))
        {
            if (health != null)
            {
                health.TakeDamage(_damage);
            }

            _canDamage = false;
        }
    }
}
