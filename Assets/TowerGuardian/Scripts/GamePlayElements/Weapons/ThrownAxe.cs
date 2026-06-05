using System;
using System.Collections;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Weapons
{
    public class ThrownAxe : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private Vector3 _positionInHand = new Vector3(0.123f, 0.054f, 0.155f);
        private Vector3 _rotationInHand = new Vector3(124, 132, -8.35f);

        private Health _playerHealth;
        private Fist _hand;
        private Collider _collider;

        private float _damage;
        private float _duration;
        private Vector3 _start;
        private Vector3 _end;
        private float _returnSpeed = 10f;
        public event Action Returned;

        private void Awake()
        {
            _hand = GetComponentInParent<Fist>();
            _playerHealth = GetComponentInParent<Health>();
            _collider = GetComponent<Collider>();

            Disable();
        }

        public void Throw(Vector3 start, Vector3 end, float duration, float damage)
        {
            transform.SetParent(null);
            _start = start;
            _end = end;
            _duration = Mathf.Max(0.01f, duration);
            _damage = damage;

            Enabled();
            transform.position = _end;

            StartCoroutine(MoveRoutine());
            _particleSystem.gameObject.SetActive(true);
        }

        private IEnumerator MoveRoutine()
        {
            float treshold = 1f;
            float elapsed = 0f;
            int rotations = 6;

            while (elapsed < _duration)
            {
                float time = elapsed / _duration;
                transform.position = Vector3.Lerp(_start, _end, time);

                float angle = rotations * 360f * time;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = _end;

            while (Vector3.SqrMagnitude(transform.position - _hand.transform.position) >= treshold * treshold)
            {
                transform.position = Vector3.Lerp(transform.position, _hand.transform.position, _returnSpeed * Time.deltaTime);

                float angle = rotations * 360f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return null;
            }

            _particleSystem.gameObject.SetActive(false);
            SetInHand();
            Returned?.Invoke();
            Disable();
        }

        private void Enabled()
        {
            enabled = true;
            _collider.enabled = true;
        }

        private void Disable()
        {
            enabled = false;
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health health))
            {
                if (health == _playerHealth)
                    return;

                health.TakeDamage(_damage);
            }
        }

        private void SetInHand()
        {
            transform.SetParent(_hand.transform);
            transform.localPosition = _positionInHand;
            transform.localRotation = Quaternion.Euler(_rotationInHand);
        }
    }
}
