using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Ability.AbilityObjects;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Configs.AbilityConfigs;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Ability
{
    public class RotateShurikens : AbilityInfrastructure.Ability
    {
        [SerializeField] private RotatingShurikenConfig _config;

        private List<Shuriken> _shurikens = new List<Shuriken>();
        private int _activeCount;
        private int _maxCount = 6;
        private float _angle;
        public override AbilityType Type => AbilityType.RotatingShuriken;
        public override AbilityConfig Config => _config;

        private void Awake()
        {
            for (int i = 0; i < _maxCount; i++)
            {
                var shuriken = Instantiate(_config.ShuricrnPrefab, transform);
                _shurikens.Add(shuriken);
                shuriken.gameObject.SetActive(false);
            }

            _config.Upgraded += Upgrade;
        }

        private void OnDestroy()
        {
            _activeCount = 0;

            foreach (var shuriken in _shurikens)
                shuriken.gameObject.SetActive(false);

            _config.Upgraded -= Upgrade;
        }

        public override void Enable()
        {
            base.Enable();
            LoadAbility();
        }

        private void Update()
        {
            if (_activeCount == 0) return;

            _angle += _config.RotationSpeed * Time.deltaTime;
            _angle %= 360f;

            float angleStep = 360f / _activeCount;

            for (int i = 0; i < _activeCount; i++)
            {
                float currentAngle = _angle + (angleStep * i);
                Vector3 pos = new Vector3(
                    Mathf.Cos(currentAngle * Mathf.Deg2Rad) * _config.Radius,
                    0f,
                    Mathf.Sin(currentAngle * Mathf.Deg2Rad) * _config.Radius);

                _shurikens[i].transform.localPosition = pos;
            }
        }

        public void Upgrade(ICardConfig useles)
        {
            LoadAbility();
        }

        private void LoadAbility()
        {
            _activeCount = Mathf.Max(1, _config.Count);

            for (int i = 0; i < _maxCount; i++)
            {
                if (i < _activeCount)
                    ActivateShuriken(i);
                else
                    _shurikens[i].gameObject.SetActive(false);
            }
        }

        private void ActivateShuriken(int index)
        {
            var shuriken = _shurikens[index];
            shuriken.gameObject.SetActive(true);
            shuriken.SetParametrs(_config.Damage, _config.SpinSpeed);
        }

        public override void Disable()
        {
            foreach (var item in _shurikens)
                item.gameObject.SetActive(false);

            _activeCount = 0;
            _angle = 0f;
            base.Disable();
        }
    }
}
