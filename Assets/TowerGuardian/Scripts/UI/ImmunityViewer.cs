using DG.Tweening;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.UI
{
    public class ImmunityViewer : MonoBehaviour
    {
        private static readonly int FresnelPowerID = Shader.PropertyToID("_FresnelPower");

        [SerializeField]
        private Health _health;
        private Renderer _targetRenderer;
        private float _defoultRenderAlfa = 0.3f;
        private float _maxRenderAlfa = 1f;
        private float _animationDuration = 0.2f;
        private Tween _fresnelTween;

        private void Awake()
        {
            _targetRenderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            _health.ImmunityObjectHited += OnHit;
            _health.ImmunityActivated += OnImmuniActivated;
            _health.ImmunityDisabled += OnImmuniDisabled;
        }

        private void OnDisable()
        {
            _fresnelTween?.Kill();

            _health.ImmunityObjectHited -= OnHit;
            _health.ImmunityActivated -= OnImmuniActivated;
            _health.ImmunityDisabled -= OnImmuniDisabled;
        }

        private void OnImmuniActivated()
        {
            _targetRenderer.enabled = true;
            _targetRenderer.material.SetFloat(FresnelPowerID, _defoultRenderAlfa);
        }

        private void OnImmuniDisabled()
        {
            _targetRenderer.material.SetFloat(FresnelPowerID, 0);
            _targetRenderer.enabled = false;
        }

        private void OnHit()
        {
            _fresnelTween?.Kill();

            _fresnelTween = DOTween.Sequence()
                .Append(DOTween.To(
                    () => _targetRenderer.material.GetFloat(FresnelPowerID),
                    value => _targetRenderer.material.SetFloat(FresnelPowerID, value),
                    _maxRenderAlfa,
                    _animationDuration))
                .Append(DOTween.To(
                    () => _targetRenderer.material.GetFloat(FresnelPowerID),
                    value => _targetRenderer.material.SetFloat(FresnelPowerID, value),
                    _defoultRenderAlfa,
                    _animationDuration));
        }
    }
}