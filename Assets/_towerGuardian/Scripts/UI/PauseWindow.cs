using DG.Tweening;
using TowerGuardian.Infrastructure;
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
        _timeService.StopGame();

        transform.localScale = Vector3.zero;

        transform.DOScale(TargetScale, ViewDuration)
            .SetEase(Ease.OutBounce)
            .SetUpdate(true);
    }

    public override void Close()
    {
        base.Close();
        _timeService.ResumeGame();
        Destroy(gameObject);
    }
}
