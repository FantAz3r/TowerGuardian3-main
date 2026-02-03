public class PauseWindow : WindowBase
{
    private ITimeService _timeService;

    protected virtual void Awake()
    {
        _timeService = ServiceLocator.Get<ITimeService>();
    }

    public override void Open()
    {
        base.Open();
        _timeService.Pause();
    }

    public override void Close()
    {
        base.Close();
        _timeService.Resume();
        Destroy(gameObject);
    }
}
