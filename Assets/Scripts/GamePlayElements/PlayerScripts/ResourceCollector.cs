using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    [SerializeField] private float _resourceFlySpeed = 2f;
    [SerializeField] private float _flyDelay = 2f;
    [SerializeField] private float _treshold = 0.5f;

    private WaitForSeconds _wait;
    private HashSet<ResourcePiece> _activeResources = new HashSet<ResourcePiece>();

    public event Action<ResourcePiece, int> Collected;

    private void Awake()
    {
        _wait = new WaitForSeconds(_flyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ResourcePiece>(out ResourcePiece resource))
        {

            if (_activeResources.Contains(resource) == false)
            {
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
        Destroy(resource.gameObject);
    }
}
