using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.Buildings
{
    [RequireComponent(typeof(Outpost))]
    public class OutpostRing : MonoBehaviour
    {
        [SerializeField] private Image _ringImage;
        private Outpost _outpost;

        private void Awake()
        {
            _outpost = GetComponent<Outpost>();
            _outpost.TimerUpdated += UpdateRing;
            _outpost.Complited += OnComplete;
        }

        private void UpdateRing(float currentTime, float targetTime)
        {
            if (_ringImage != null && targetTime > 0)
            {
                _ringImage.fillAmount = Mathf.Clamp01(currentTime / targetTime);
            }
        }

        private void OnComplete()
        {
            _outpost.Complited -= OnComplete;
            _outpost.TimerUpdated -= UpdateRing;
        }
    }
}
