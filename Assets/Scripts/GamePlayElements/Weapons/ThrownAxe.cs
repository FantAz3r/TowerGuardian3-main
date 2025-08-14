using System;
using System.Collections;
using UnityEngine;

public class ThrownAxe : MonoBehaviour
{
    private Fist _hand;
    private float _damage;
    private Transform _owner;
    private float _duration;
    private Vector3 _start;
    private Vector3 _end;
    private float _returnSpeed = 10f;

    public event Action Returned;

    private void Awake()
    {
        _hand = GetComponentInParent<Fist>();   
        enabled = false;
    }

    public void Init(Transform owner, Vector3 start, Vector3 end, float duration, float damage)
    {
        enabled = true;
        _owner = owner;
        _start = start;
        _end = end;
        _duration = Mathf.Max(0.01f, duration);
        _damage = damage;

        transform.position = _end;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        float treshold = 0.5f;
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            float time = elapsed / _duration; 
            transform.position = Vector3.Lerp(_start, _end, time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = _end;
        Debug.Log("Reached target");

        while (Vector3.SqrMagnitude(transform.position - _hand.transform.position) >= treshold * treshold)
        {
            transform.position = Vector3.Lerp(transform.position, _hand.transform.position, _returnSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = _hand.transform.position;
        Debug.Log("Returned");

        Returned?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_owner != null)
        {
            var root = other.transform.root;
            if (root == _owner.root) return;
        }

        if (other.TryGetComponent<IDemageable>(out IDemageable health))
        {
            health.TakeDamage(_damage);
            return;
        }
    }
}