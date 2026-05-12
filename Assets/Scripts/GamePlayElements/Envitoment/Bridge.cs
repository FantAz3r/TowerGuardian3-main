using DG.Tweening;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [Header("Параметры моста")]
    private float _raisedAngle = -90f;
    private float _loweredAngle = 12.5f;
    private float _duration = 1.5f;

    private Tweener _currentTween;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(new Vector3(_raisedAngle, transform.eulerAngles.y, transform.eulerAngles.z));
    }

    private void OnDestroy()
    {
        _currentTween?.Kill();
    }

    public void RaiseBridge()
    {
        _currentTween?.Kill();
        _currentTween = transform
            .DORotate(new Vector3(_raisedAngle, transform.eulerAngles.y, transform.eulerAngles.z), _duration)
            .SetEase(Ease.OutQuad);
    }

    public void LowerBridge()
    {
        _currentTween?.Kill();
        _currentTween = transform
            .DORotate(new Vector3(_loweredAngle, transform.eulerAngles.y, transform.eulerAngles.z), _duration)
            .SetEase(Ease.OutQuad);
    }
}
