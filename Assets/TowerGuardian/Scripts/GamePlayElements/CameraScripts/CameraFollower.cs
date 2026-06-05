using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.CameraScripts
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Vector3 _offsetPosition = new Vector3(0, 10, -10);
        [SerializeField] private Vector3 _rotation = new Vector3(40, 0, 0);

        private Transform _target;
        private Vector3 _currentPosition;
        private Health _playerHealth;


        public void Init(Transform target)
        {
            _target = target;
            _playerHealth = target.GetComponent<Health>();

            if (_playerHealth != null)
            {
                _playerHealth.Destroyed += StopFollow;
            }
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            transform.position = _target.position + _offsetPosition;
            _currentPosition = _target.position;
        }

        public void StopFollow()
        {
            if (_playerHealth != null)
            {
                _playerHealth.Destroyed -= StopFollow;
            }

            _target = null;
        }
    }
}