using System;
using UnityEngine;

public abstract class Quest : IQuest
{
    private bool _isProgressQuest;
    private bool _isTimeQuest;
    private QuestViewer _questViewer;
    private IGameConditionService _conditionService;
    private IWindowService _windowService;
    public QuestConfig Config { get; private set; }
    public float CurrentTime { get; protected set; } = 0;
    public int CurrentValue { get; protected set; } = 0;

    public event Action OnCompleted;
    public void SetConfig(QuestConfig config)
    {
        Config = config;
        _isProgressQuest = Config.IsProgressQuest;
        _isTimeQuest = Config.IsTimeQuest;

        _windowService = ServiceLocator.Get<IWindowService>();
        _conditionService = ServiceLocator.Get<IGameConditionService>();
    }

    public abstract QuestType GetQuestType();

    public virtual void Run() 
    {
        _questViewer = _windowService.Open(WindowType.QuestViewer) as QuestViewer;
        _questViewer.Render(this);
        UpdateProgress();
        UpdateTime();
    }

    public virtual Vector3 TryGetTarget() => Vector3.zero;

    public virtual void UpdateProgress() 
    {
        if (_isProgressQuest)
            _questViewer.UpdateProgress(CurrentValue, Config.TargetValue);
    }

    public virtual void UpdateTime()
    {
        if (_isTimeQuest)
            _questViewer.UpdateTime(CurrentTime);
    }

    public virtual void Stop() { }

    public virtual void Complete()
    {
        OnCompleted?.Invoke();
        _questViewer.Close();
    }

    public virtual void Fail()
    {
        _conditionService.OnLouse();
    }
}