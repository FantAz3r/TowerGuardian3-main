using System;
using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies
{
    public class TargetDetector : MonoBehaviour
    {
        private PortalFrame _portalFrame;
        private Player _player;

        private List<Transform> _targets = new List<Transform>();

        public event Action<Transform> TargetDetected;

        public event Action<Transform> TargetLost;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PortalFrame portalFrame))
            {
                if (portalFrame.IsActive)
                {
                    _portalFrame = portalFrame;

                    if (!_targets.Contains(portalFrame.transform))
                    {
                        _targets.Add(_portalFrame.transform);
                    }

                    _portalFrame.Disabled += OnTargetDisabled;
                }
            }

            if (other.TryGetComponent(out Player player))
            {
                _player = player;

                if (!_targets.Contains(_player.transform))
                {
                    _targets.Add(_player.transform);
                }
            }

            UpdateTarget();
        }

        private void OnTargetDisabled()
        {
            _portalFrame.Disabled -= OnTargetDisabled;
            _targets.Remove(_portalFrame.transform);

            if (_player != null)
            {
                TargetLost?.Invoke(_player.transform);
            }
        }

        private void UpdateTarget()
        {
            foreach (Transform target in _targets)
            {
                if (target.TryGetComponent<PortalFrame>(out _))
                {
                    TargetDetected?.Invoke(target);
                    return;
                }
            }

            if (_player != null)
            {
                TargetDetected?.Invoke(_player.transform);
            }
        }
    }
}
