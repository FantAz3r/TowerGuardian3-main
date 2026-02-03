using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShurikens : Ability
{
    [SerializeField] private RotatingShurikenConfig _config;

    private List<Shuriken> _shurikens = new List<Shuriken>();
    private Coroutine _rotateCoroutine;
    private int _activeCount = 0;
    private int _maxCount = 6;
    public override AbilityType Type => AbilityType.RotatingShuriken;

    public override AbilityConfig Config => _config;

    private void Awake()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            var shuriken = Instantiate(_config.Prefab, transform);
            _shurikens.Add(shuriken);
            shuriken.gameObject.SetActive(false);
        }

        _config.Upgraded += Upgrade;
    }

    private void OnDisable()
    {
        _activeCount = 0;

        if (_rotateCoroutine != null)
        {
            StopCoroutine(_rotateCoroutine);
        }

        _config.Upgraded -= Upgrade;
    }

    public override void Enable()
    {
        _rotateCoroutine = StartCoroutine(Rotate());
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

    public void Upgrade()
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

    private void ActivateShuriken(int index)
    {
        var shuriken = _shurikens[index];
        shuriken.gameObject.SetActive(true);
        shuriken.SetParametrs(_config.Damage, _config.SpinSpeed);
    }

    public override void Remove()
    {
        foreach(var item in _shurikens)
        {
            item.gameObject.SetActive(false);
        }

        _activeCount = 0;
    }
}
