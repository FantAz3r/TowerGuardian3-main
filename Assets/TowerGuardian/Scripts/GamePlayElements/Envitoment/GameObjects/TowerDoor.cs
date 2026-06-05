using System;
using DG.Tweening;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class TowerDoor : MonoBehaviour
    {
        [SerializeField] private float openHeight = 3f;
        [SerializeField] private float duration = 1f;

        private bool _isOpen;
        private Vector3 _closedPosition;

        public event Action Opened;

        private void Awake()
        {
            _closedPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isOpen)
                return;

            if (other.TryGetComponent<Player>(out _))
            {
                _isOpen = true;
                OpenDoor();
                Opened?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                _isOpen = false;
                CloseDoor();
                Opened?.Invoke();
            }
        }

        private void OpenDoor()
        {
            transform.DOMoveY(_closedPosition.y - openHeight, duration).SetEase(Ease.OutQuad);
        }

        private void CloseDoor()
        {
            transform.DOMoveY(_closedPosition.y, duration).SetEase(Ease.InQuad);
        }
    }
}
