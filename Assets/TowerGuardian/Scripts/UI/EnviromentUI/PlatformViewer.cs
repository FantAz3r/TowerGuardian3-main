using System.Collections;
using TMPro;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.EnviromentUI
{
    [RequireComponent(typeof(Platform))]

    public class PlatformViewer : MonoBehaviour
    {
        [SerializeField] private Transform _button;
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private Image _timerViewer;
        [SerializeField] private TMP_Text _text;

        private Vector3 _downPosition;
        private Vector3 _upPosition;

        private float _offset = 0.1f;
        private Platform _platform;

        private void Awake()
        {
            _upPosition = new Vector3(transform.position.x, transform.position.y + _offset, transform.position.z);
            _downPosition = transform.position;

            _platform = GetComponent<Platform>();
            _platform.PlayerEnteredZone += OnPlayerEnteredZone;
            _platform.PlayerExitedZone += OnPlayerExitedZone;
            _platform.TimerUpdated += OnTimerUpdated;

            _text.text = UIText.GetWindowTypeText(_platform.WindowType);
            _timerViewer.fillAmount = 0;
            MovePlatform(_upPosition);
        }

        private void OnDestroy()
        {
            _platform.PlayerEnteredZone -= OnPlayerEnteredZone;
            _platform.PlayerExitedZone -= OnPlayerExitedZone;
            _platform.TimerUpdated -= OnTimerUpdated;
        }

        private void OnPlayerEnteredZone()
        {
            StopAllCoroutines();
            StartCoroutine(MovePlatformRoutine(_downPosition));
        }

        private void OnPlayerExitedZone()
        {
            StopAllCoroutines();
            StartCoroutine(MovePlatformRoutine(_upPosition));
        }

        private void OnTimerUpdated(float currentTime, float interactionTime)
        {
            float fillAmount = Mathf.Clamp01(currentTime / interactionTime);
            _timerViewer.fillAmount = fillAmount;
        }

        private IEnumerator MovePlatformRoutine(Vector3 targetPos)
        {
            float treshold = 0.01f;

            while (Vector3.SqrMagnitude(_button.position - targetPos) > treshold * treshold)
            {
                _button.position = Vector3.MoveTowards(_button.position, targetPos, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            _button.position = targetPos;
        }

        private void MovePlatform(Vector3 position)
        {
            _button.position = position;
        }
    }
}