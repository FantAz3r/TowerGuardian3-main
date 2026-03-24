using System;
using UnityEngine;
using YG;

public abstract class Quest : IQuest
{
    private bool _isProgressQuest;
    private bool _isTimeQuest;
    private IGameConditionService _conditionService;
    private IWindowService _windowService;
    private IScoreService _service;

    public QuestViewer QuestViewer { get; private set; }
    public QuestConfig Config { get; private set; }
    public float CurrentTime { get; protected set; } = 0;
    public int CurrentValue { get; protected set; } = 0;
    public bool CanStop { get; protected set; } = true;

    public event Action OnCompleted;

    public void SetConfig(QuestConfig config)
    {
        Config = config;
        _isProgressQuest = Config.IsProgressQuest;
        _isTimeQuest = Config.IsTimeQuest;

        _service = ServiceLocator.Get<IScoreService>();
        _windowService = ServiceLocator.Get<IWindowService>();
        _conditionService = ServiceLocator.Get<IGameConditionService>();
    }

    public abstract QuestType GetQuestType();

    public virtual void Run() 
    {
        YG2.onSwitchLang += On—hangeLang;
        QuestViewer = _windowService.Open(WindowType.QuestViewer) as QuestViewer;
        QuestViewer.Render(this);
        UpdateProgress();
        UpdateTime();
    }

    public virtual Vector3 TryGetTarget() => Vector3.zero;

    public virtual void UpdateProgress() 
    {
        UpdateProgress(CurrentValue, Config.TargetValue);
    }

    public virtual void UpdateProgress(float currentValue, float targetValue)
    {
        if (_isProgressQuest)
            QuestViewer.UpdateProgress(currentValue, targetValue);
    }

    public virtual void UpdateTime()
    {
        if (_isTimeQuest)
            QuestViewer.UpdateTime(CurrentTime);
    }

    public virtual void Stop() 
    {
        YG2.onSwitchLang -= On—hangeLang;
    }

    public virtual void Complete()
    {
        _service.AddScore(ScoreType.Quest, Config.ScorePoints);

        YG2.onSwitchLang -= On—hangeLang;
        QuestViewer.Close();
        OnCompleted?.Invoke();
    }

    public virtual void Fail()
    {
        YG2.onSwitchLang -= On—hangeLang;
        _conditionService.OnLouse(); 
    }

    private void On—hangeLang(string useles)
    {
        if(QuestViewer != null)
        {
            QuestViewer.Render(this);
        }
    }
}