using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI
{
    public class Highlighter : MonoBehaviour
    {
        [SerializeField] private Image _warningFrame;
        private Tween _warningTween;
       
        private void OnDestroy()
        {
            _warningTween?.Kill();
        }

        public void ActivateWarning()
        {
            if (_warningFrame == null)
                return;

            _warningFrame.enabled = true;

            _warningTween?.Kill();

            _warningTween = _warningFrame.material.DOFade(0f, 0.3f)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void DeactivateWarning()
        {
            if (_warningFrame == null)
                return;

            _warningTween?.Kill();
            _warningFrame.enabled = false;
        }
    }
}