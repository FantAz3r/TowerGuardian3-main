using System;
using System.Collections;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private int _damage;
    private float _speenSpeed;
    private float _updateTime = 0.05f;
    private WaitForSeconds _delay;
    private Coroutine _rotateRoutine;

    public event Action<int> DialedDamage;

    private void Awake()
    {
        _delay = new WaitForSeconds(_updateTime);
    }

    private void OnDisable()
    {
        if (_rotateRoutine != null)
        {
            StopCoroutine(_rotateRoutine);
        }
    }

    public void SetParametrs(int damage, float speenSpeed)
    {
        if(_rotateRoutine != null)
        {
            StopCoroutine(_rotateRoutine);
        }

        _damage = damage;
        _speenSpeed = speenSpeed;
        _rotateRoutine = StartCoroutine(SpeenRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health demageable))
        {
            int damage = (int)Mathf.Min(_damage, demageable.CurrentHealth);
            demageable.TakeDamage(damage);
            DialedDamage?.Invoke(damage);
        }
    }

    private IEnumerator SpeenRoutine()
    {
        while (enabled)
        {
            transform.Rotate(0, _speenSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}
