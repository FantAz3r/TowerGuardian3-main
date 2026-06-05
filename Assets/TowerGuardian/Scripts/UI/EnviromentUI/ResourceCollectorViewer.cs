using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using UnityEngine;

namespace TowerGuardian.Scripts.UI.EnviromentUI
{
    public class ResourceCollectorViewer : MonoBehaviour
    {
        [SerializeField] private ResourceCollector _resourceCollector;
        private RectTransform _circle;

        private void Awake()
        {
            _circle = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _resourceCollector.RangeSeted += View;
        }

        private void OnDisable()
        {
            _resourceCollector.RangeSeted -= View;
        }

        private void View(float radius)
        {
            float coefficient = 1.2f;
            _circle.sizeDelta = new Vector2(radius * coefficient, radius * coefficient);
        }
    }
}