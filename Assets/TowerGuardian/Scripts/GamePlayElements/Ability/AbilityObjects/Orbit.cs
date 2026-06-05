using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Projectiles;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityObjects
{
    public class Orbit : MonoBehaviour
    {
        private float _firstOrbitRadius = 3;
        private float _radius = 3;
        private float _targetRadius;
        private float _increaseRadiusValue = 3;
        private Vector3 _direction;
        private float _baseRotationSpeed = 150f;
        private float _rotationSpeed;
        private List<LavaRock> _rocks = new();
        private bool _isInited;
        private int _orbitIndex;

        private ISpawnerService _spawnerService;
        private float[] _possibleAngles = { 0f, 90f, 180f, 270f };

        public void Init(int rocksCount, int damage)
        {
            _spawnerService = ServiceLocator.Get<ISpawnerService>();
            _direction = Random.value > 0.5f ? Vector3.up : Vector3.down;

            _radius = 5f;
            _targetRadius = _radius;

            _rotationSpeed = _baseRotationSpeed;
            SpawnProjectiles(rocksCount, damage);
            _isInited = true;
        }

        private void Update()
        {
            if (!_isInited)
                return;

            if (Mathf.Abs(_radius - _targetRadius) > 0.01f)
            {
                _radius = Mathf.MoveTowards(_radius, _targetRadius, Time.deltaTime);
                UpdateRotationSpeed();
                UpdateProjectilesPositions();
            }

            transform.Rotate(_direction * _rotationSpeed * Time.deltaTime);
        }

        private void UpdateRotationSpeed()
        {
            _rotationSpeed = _baseRotationSpeed * (_radius == 0 ? 1f : 5f / _radius);
        }

        public void IncreaseOrbitRange()
        {
            _orbitIndex++;
            _targetRadius = _firstOrbitRadius + (_orbitIndex * _increaseRadiusValue);
        }

        private void SpawnProjectiles(int count, int damage)
        {
            List<float> availableAngles = new List<float>(_possibleAngles);

            for (int i = 0; i < count; i++)
            {
                int idx = Random.Range(0, availableAngles.Count);
                float angle = availableAngles[idx];
                availableAngles.RemoveAt(idx);

                LavaRock rock = _spawnerService.SendProjectileRequest(ProjectileType.LavaRock, transform.position, transform) as LavaRock;
                rock.Init(damage);
                rock.transform.SetParent(transform);
                _rocks.Add(rock);

                Vector3 pos = AngleToPosition(angle, _radius);
                rock.transform.localPosition = pos;
            }
        }

        private void UpdateProjectilesPositions()
        {
            for (int i = 0; i < _rocks.Count; i++)
            {
                _rocks[i].transform.localPosition = AngleToPosition(_possibleAngles[i], _radius);
            }
        }

        private Vector3 AngleToPosition(float angleDegrees, float radius)
        {
            float rad = angleDegrees * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad) * radius;
            float z = Mathf.Sin(rad) * radius;
            return new Vector3(x, 0f, z);
        }

        public void RemoveOrbit()
        {
            foreach (var rock in _rocks)
            {
                if (rock != null)
                {
                    rock.gameObject.SetActive(false);
                    rock.transform.SetParent(null);
                }
            }

            _rocks.Clear();
        }
    }
}
