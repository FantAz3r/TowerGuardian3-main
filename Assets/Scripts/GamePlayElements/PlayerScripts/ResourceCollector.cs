using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollector : MonoBehaviour, IBuffble
{
    [SerializeField] private float _resourceFlySpeed = 2f;
    [SerializeField] private float _flyDelay = 2f;
    [SerializeField] private float _treshold = 0.5f;

    private WaitForSeconds _wait;
    private HashSet<ResourcePiece> _activeResources = new HashSet<ResourcePiece>();
    private SphereCollider _collectionCollider;
    private float _startRange;

    public event Action<float> RangeSeted;
    public event Action<ResourcePiece, int> Collected;

    private void Awake()
    {
        _wait = new WaitForSeconds(_flyDelay);
        _collectionCollider = GetComponent<SphereCollider>();
        _startRange = _collectionCollider.radius;
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

        Collected?.Invoke(resource, resource.Amount);
        _activeResources.Remove(resource);
        resource.gameObject.SetActive(false);
    }

    public void ApplyBuff(float value)
    {
        _collectionCollider.radius = _startRange * (1 + value);
        RangeSeted?.Invoke(_collectionCollider.radius);
    }
}
