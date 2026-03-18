using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPickaxe : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private Vector3 _positionInHand = new Vector3(0.123f, 0.054f, 0.155f);
    private Vector3 _rotationInHand = new Vector3(0, 38, -100);

    private Fist _hand;
    private Health _currentTarget = null;
    private List<Health> _hitedTargets = new List<Health>();

    private int _currentHitCount = 0;
    private int _maxHitCount;
    private float _damage;
    private float _flySpeed;
    private float _searchRange;

    public event Action<int> Returned;

    private void Awake()
    {
        _hand = GetComponentInParent<Fist>();
    }

    public void Throw(int bounceCount, float searchRange, float damage, float flySpeed)
    {
        _damage = damage;
        _flySpeed = flySpeed;
        _maxHitCount = bounceCount;
        _searchRange = searchRange;

        transform.SetParent(null);
        _particleSystem.gameObject.SetActive(true);
        NextMove();
    }

    private void OnHit(Health target)
    {
        _currentHitCount++;

        if (target != null)
        {
            target.TakeDamage(_damage);
            _hitedTargets.Add(target);
        }

        _currentTarget = null;
        NextMove();
    }

    private IEnumerator FlyRoutine(Transform target)
    {
        float threshold = 0.1f;
        Vector3 offset = new Vector3(0, 1, 0);
        int rotations = 4;
        float totalAngle = 0f;

        while (Vector3.SqrMagnitude(transform.position - (target.position + offset)) > threshold * threshold)
        {
            Vector3 targetPos = target.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _flySpeed * Time.deltaTime);

            float angleStep = rotations * 360f * Time.deltaTime;
            totalAngle += angleStep;
            transform.rotation = Quaternion.Euler(0, 0, totalAngle);

            yield return null;
        }

        transform.position = target.position + offset;

        if (_currentTarget != null)
        {
            OnHit(_currentTarget);

        }
        else
        {
            SetInHand();
        }
    }

    private void NextMove()
    {
        _currentTarget = TryFindTarget();

        if (_currentHitCount >= _maxHitCount || _currentTarget == null)
        {
            StartCoroutine(FlyRoutine(_hand.transform));
        }
        else
        {
            StartCoroutine(FlyRoutine(_currentTarget.transform));
        }
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

        foreach (var target in _hitedTargets)
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
        _particleSystem.gameObject.SetActive(false);
        transform.SetParent(_hand.transform);
        transform.localPosition = _positionInHand;
        transform.localRotation = Quaternion.Euler(_rotationInHand);
        Returned?.Invoke(_currentHitCount);
        _hitedTargets.Clear();
        _currentHitCount = 0;
    }
}
