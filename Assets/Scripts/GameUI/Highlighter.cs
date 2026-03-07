using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private Image _warningFrame;
    private Tween _warningTween;

    public void ActivateWarning()
    {
        if (_warningFrame == null)
            return;

        _warningFrame.enabled = true;

        _warningTween?.Kill();

        _warningTween = _warningFrame.DOFade(0f, 0.3f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void DeactivateWarning()
    {
        _warningTween?.Kill();
        _warningFrame.enabled = false;
    }
}
