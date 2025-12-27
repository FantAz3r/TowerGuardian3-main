using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShurikens : Ability, IDamageAbility
{
    [SerializeField] private RotatingShurikenConfig _config;

    private List<Shuriken> _shurikens = new List<Shuriken>();
    private Coroutine _rotateCoroutine;
    private int _activeCount = 0;
    private int _maxCount = 6;

    public event Action<float> DialedDamage;

    public override AbilityType AbilityType => AbilityType.RotatingShuriken;

    private void Awake()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            var shuriken = Instantiate(_config.Prefab, transform);
            _shurikens.Add(shuriken);
            shuriken.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        foreach (var shuricen in _shurikens)
        {
            shuricen.DialedDamage -= OnHit;
        }

        _activeCount = 0;

        if (_rotateCoroutine != null)
        {
            StopCoroutine(_rotateCoroutine);
        }
    }

    public override void Enable()
    {
        _rotateCoroutine = StartCoroutine(Rotate());
        base.Enable();
        LoadAbillity();
    }

    private IEnumerator Rotate()
    {
        float angle = 0f;

        while (enabled)
        {
            angle += _config.RotationSpeed * Time.deltaTime;
            angle %= 360f;

            if (_activeCount == 0) yield return null;

            float angleStep = 360f / _activeCount;

            for (int i = 0; i < _activeCount; i++)
            {
                float currentAngle = angle + angleStep * i;
                Vector3 pos = new Vector3(
                    Mathf.Cos(currentAngle * Mathf.Deg2Rad) * _config.Radius,
                    0f,
                    Mathf.Sin(currentAngle * Mathf.Deg2Rad) * _config.Radius);

                _shurikens[i].transform.localPosition = pos;
            }

            yield return null;
        }
    }

    public override void Upgrade()
    {
        LoadAbillity();
    }

    private void UpdatePositions()
    {
        float angleStep = 360f / _activeCount;

        for (int i = 0; i < _activeCount; i++)
        {
            float currentAngle = angleStep * i;
            Vector3 pos = new Vector3(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad) * _config.Radius,
                0f,
                Mathf.Sin(currentAngle * Mathf.Deg2Rad) * _config.Radius);

            _shurikens[i].transform.localPosition = pos;
        }
    }

    private void LoadAbillity()
    {
        _activeCount = _config.Count;

        for (int i = 0; i < _shurikens.Count; i++)
        {
            if (i < _activeCount)
            {
                ActivateShuriken(i);
            }
            else
            {
                _shurikens[i].gameObject.SetActive(false);
            }
        }

        UpdatePositions();
    }

    public void OnHit(int damage)
    {
        DialedDamage?.Invoke(damage);
    }

    private void ActivateShuriken(int index)
    {
        var shuriken = _shurikens[index];
        shuriken.gameObject.SetActive(true);
        shuriken.SetParametrs(_config.Damage, _config.SpinSpeed);
        shuriken.DialedDamage += OnHit;
    }

    public override void Remove()
    {
        foreach(var item in _shurikens)
        {
            item.gameObject.SetActive(false);
        }

        _activeCount = 0;
        base.Remove();
    }
}
