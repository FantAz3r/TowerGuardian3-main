using System;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Scores;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.UI.Elements;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.Quests.QuestInfrastructure
{
    public abstract class Quest : IQuest
    {
        private bool _isProgressQuest;
        private bool _isTimeQuest;
        private IGameConditionService _conditionService;
        private IWindowService _windowService;
        private IScoreService _service;

        public event Action OnCompleted;

        public bool CanStop { get; protected set; } = true;
        public QuestConfig Config { get; private set; }
        protected QuestViewer QuestViewer { get; private set; }
        protected float CurrentTime { get; set; } = 0;
        protected int CurrentValue { get; set; } = 0;

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
            YG2.onSwitchLang += OnChangeLang;
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
            YG2.onSwitchLang -= OnChangeLang;
        }

        public virtual void Complete()
        {
            _service.AddScore(ScoreType.Quest, Config.ScorePoints);

            YG2.onSwitchLang -= OnChangeLang;
            QuestViewer.Close();
            OnCompleted?.Invoke();
        }

        public virtual void Fail()
        {
            YG2.onSwitchLang -= OnChangeLang;
            _conditionService.OnLouse();
        }

        private void OnChangeLang(string useles)
        {
            if (QuestViewer != null)
            {
                QuestViewer.Render(this);
            }
        }
    }
}