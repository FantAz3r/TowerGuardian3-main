using UnityEngine;
using System.Collections;

public class ThrownAxe : MonoBehaviour
{
    private float _damage;
    private Transform _owner; 
    private float _duration;
    private Vector3 _start;
    private Vector3 _end;
    private float _a;
    private float _b;
    private Vector3 _forward;
    private bool _isActive;

    private void Awake()
    {
        enabled = false;
    }

    public void Init(Transform owner, Vector3 start, Vector3 end, float duration, float height, float damage)
    {
        enabled = true;
        _owner = owner;
        _start = start;
        _end = end;
        _duration = Mathf.Max(0.01f, duration);
        _a = Vector3.Distance(start, end) / 2f;
        _b = Mathf.Max(0f, height);
        _forward = (end - start).normalized;
        _damage = damage;

        transform.position = _start;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        _isActive = true;
        float elapsed = 0f;
        Vector3 center = (_start + _end) / 2f;
        while (elapsed < _duration)
        {
            float t = elapsed / _duration; 
            float theta = Mathf.Lerp(0f, Mathf.PI, t); 
            Vector3 pos = center + _forward * (_a * Mathf.Cos(theta)) + Vector3.up * (_b * Mathf.Sin(theta));
            transform.position = pos;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = _end;
        _isActive = false;

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive == false) return;

        if (_owner != null)
        {
            var root = other.transform.root;
            if (root == _owner.root) return;
        }
          
        if (other.TryGetComponent<IDemageable>(out IDemageable health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
            return;
        }
    }
}