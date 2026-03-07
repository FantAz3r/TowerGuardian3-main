using DG.Tweening;
using UnityEngine;

public class PauseWindow : WindowBase
{
    private const float ViewDuration = 0.5f;
    private const float TargetScale = 1f;

    private ITimeService _timeService;

    protected virtual void Awake()
    {
        _timeService = ServiceLocator.Get<ITimeService>();
    }

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0;

        transform.localScale = Vector3.zero;

        transform.DOScale(TargetScale, ViewDuration)
            .SetEase(Ease.OutBounce)
            .SetUpdate(true);
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
