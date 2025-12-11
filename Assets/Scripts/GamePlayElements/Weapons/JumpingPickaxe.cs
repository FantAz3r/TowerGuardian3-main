using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPickaxe : MonoBehaviour
{
    private Vector3 _positionInHand = new Vector3(0.123f, 0.054f, 0.155f);
    private Vector3 _rotationInHand = new Vector3(124, 132, -8.35f);

    private Fist _hand;
    private Health _currentTarget = null;
    private List<Health> _hitedTargets = new List<Health>();

    private int _currentHitCount = 0;
    private int _maxHitCount;
    private int _damage;
    private float _flySpeed;
    private float _searchRange;

    public event Action<int> Returned;

    private void Awake()
    {
        _hand = GetComponentInParent<Fist>();
    }

    public void Throw(int bounceCount, float searchRange, int damage, float flySpeed)
    {
        _damage = damage;
        _flySpeed = flySpeed;
        _maxHitCount = bounceCount;
        _searchRange = searchRange;

        transform.SetParent(null);
        NextMove();
    }

    private void OnHit(Health target)
    {
        _currentHitCount++;

        var health = target.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(_damage);
        }

        NextMove();
    }

    private IEnumerator FlyRoutine(Transform target)
    {
        float treshold = 0.05f;

        while (Vector3.SqrMagnitude(transform.position - target.position) > treshold * treshold)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, _flySpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target.position;

        if (_currentTarget != null)
        {
            OnHit(_currentTarget);
            _currentTarget = null;
        }
        else
        {
            SetInHand();
        }
    }

    private void NextMove()
    {
        _currentTarget = TryFindTarget();
        _hitedTargets.Add(_currentTarget);

        if (_currentHitCount >= _maxHitCount && _currentTarget == null)
        {
            StartCoroutine(FlyRoutine(_hand.transform));
        }
        else
        {
            StartCoroutine(FlyRoutine(_currentTarget.transform));
        }
            Debug.Log(_currentTarget);
            Debug.Log(_currentHitCount);
    }

    private Health TryFindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRange);
        List<Health> candidates = new List<Health>();

        foreach (var colider in colliders)
        {
            if (colider.TryGetComponent(out Health health))
            {
                candidates.Add(health);
            }
        }

        List<Health> healthObjects = Utils.GetObjectsSortedByDistance(candidates, transform.position);

        foreach(var target in _hitedTargets)
        {
            if (healthObjects.Contains(target))
            {
                healthObjects.Remove(target);
            }
        }

        foreach (var item in healthObjects)
        {
            if (item.GetHealthType() == EntityType.Enemy)
                return item;
        }

        foreach (var item in healthObjects)
        {
            if (item.GetHealthType() == EntityType.Stone)
                return item;
        }

        return null;
    }

    private void SetInHand()
    {
        transform.SetParent(_hand.transform);
        transform.localPosition = _positionInHand;
        transform.localRotation = Quaternion.Euler(_rotationInHand);
        Returned?.Invoke(_currentHitCount);
        _currentHitCount = 0;
    }
}
