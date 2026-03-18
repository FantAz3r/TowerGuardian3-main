using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollector : MonoBehaviour, IBuffble
{
    [SerializeField] private float _resourceFlySpeed = 10f;
    [SerializeField] private float _flyDelay = 1f;
    [SerializeField] private float _treshold = 0.5f;

    private StatsCalculator _statsCalculator;
    private HashSet<ResourcePiece> _activeResources = new HashSet<ResourcePiece>();
    private WaitForSeconds _wait;
    private SphereCollider _collectionCollider;
    private float _startRange;
    private ISpawnerService _spawnerService;

    public event Action<float> RangeSeted;
    public event Action<ResourcePiece> Collected;

    private void Awake()
    {
        _statsCalculator = new StatsCalculator();
        _collectionCollider = GetComponent<SphereCollider>();
        _wait = new WaitForSeconds(_flyDelay);
        _startRange = _collectionCollider.radius;
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
    }

    private void Start()
    {
        RangeSeted?.Invoke(_collectionCollider.radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourcePiece resource))
        {
            if (_activeResources.Contains(resource) == false)
            {
                resource.OnTake();
                _activeResources.Add(resource);
                StartCoroutine(RelocateResource(resource));
            }
        }
    }

    private IEnumerator RelocateResource(ResourcePiece resource)
    {
        float sqrThreshold = _treshold * _treshold;
        yield return _wait;

        while ((resource.transform.position - transform.position).sqrMagnitude > sqrThreshold)
        {
            resource.transform.position = Vector3.MoveTowards(resource.transform.position, transform.position, _resourceFlySpeed * Time.deltaTime);
            yield return null;
        }

        Collected?.Invoke(resource);
        _activeResources.Remove(resource);
        resource.gameObject.SetActive(false);
        _spawnerService.SendSoundReqest(resource.CollectSound, transform.position);
    }

    public void EnableBuff()
    {
    }

    public void ApplyBuff(IEffect effect)
    {
        _statsCalculator.AddEffect(effect);
        _collectionCollider.radius = _statsCalculator.Calculate(_startRange);
        RangeSeted?.Invoke(_collectionCollider.radius);
    }

    public void Recalculate()
    {
        _collectionCollider.radius = _statsCalculator.Calculate(_startRange);
        RangeSeted?.Invoke(_collectionCollider.radius);
    }

    public void RemoveBuff(IEffect effect)
    {
        _statsCalculator.RemoveEffect(effect);
        _collectionCollider.radius = _statsCalculator.Calculate(_startRange);
        RangeSeted?.Invoke(_collectionCollider.radius);
    }
}
