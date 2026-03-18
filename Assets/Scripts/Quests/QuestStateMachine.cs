using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class QuestStateMachine : MonoBehaviour
{
    private List<IQuest> _quests = new();
    private QuestBuilder _builder;
    private int _currentQuestIndex = -1;
    private int _startQuestIndex = -1;
    private LevelID _level;
    private IQuest _currentQuest;
    private IGameConditionService _conditionService;
    private bool _isAllQuestsComplete = false;

    public event Action AllQuestsCompleted, QuestCompleted;
    public event Action<IQuest> QuestStarted;

    public void Init(QuestBuilder builder, LevelID level, IReadOnlyList<QuestType> questsForThisLevel)
    {
        _level = level;
        _builder = builder;

        foreach (var questType in questsForThisLevel)
        {
            _quests.Add(_builder.GetQuest(questType));
        }

        LoadQuestProgress();

        _conditionService = ServiceLocator.Get<IGameConditionService>();
    }

    private void OnDestroy()
    {
        _currentQuest?.Stop();
    }

    public void Run()
    {
        SwitchQuest();
    }

    public void SetQuest(QuestType questType = default)
    {
        SwitchQuest(questType);
    }

    private void SwitchQuest(QuestType questType = default)
    {
        if (_currentQuest != null)
        {
            _currentQuest.Stop();
            _currentQuest.OnCompleted -= OnQuestCompleted;
        }

        if (questType != default)
        {
            _currentQuest = _builder.GetQuest(questType);
        }
        else
        {
            _currentQuestIndex++;

            if (_currentQuestIndex >= _quests.Count)
            {
                _currentQuest = null;
                AllQuestsCompleted?.Invoke();
                _isAllQuestsComplete = true;
                return;
            }
            
            if(_currentQuestIndex < 0)
            {
                _currentQuestIndex = 0; 
            }

            _currentQuest = _quests[_currentQuestIndex];
        }

        _currentQuest.OnCompleted += OnQuestCompleted;
        _currentQuest.Run();
        QuestStarted?.Invoke(_currentQuest);

        SaveQuestProgress();
    }

    private void OnQuestCompleted()
    {
        SaveQuestProgress();
        QuestCompleted?.Invoke();
        _currentQuest.OnCompleted -= OnQuestCompleted;
        SwitchQuest();
    }

    private void SaveQuestProgress()
    {
        YG2.saves.QuestProgress ??= new List<QuestSaveData>();

        if (_level != LevelID.Tower)
            return;
        
        int index = YG2.saves.QuestProgress.FindIndex(quest => quest.Level == _level);

        QuestSaveData saveData;

        if (_isAllQuestsComplete == false)
        {
            saveData = new QuestSaveData(_level, 0, 0, _currentQuestIndex);
        }
        else
        {
            saveData = new QuestSaveData(_level, 0, 0, _startQuestIndex);
        }

        if (index == -1)
        {
            YG2.saves.QuestProgress.Add(saveData);
        }
        else
        {
            YG2.saves.QuestProgress[index] = saveData;
        }

        YG2.SaveProgress();
    }

    private void LoadQuestProgress()
    {
        if (YG2.saves.QuestProgress == null)
        {
            _currentQuestIndex = -1;
            return;
        }

        var saveData = YG2.saves.QuestProgress.Find(quest => quest.Level == _level);

        if (saveData.Level == LevelID.None)
        {
            _currentQuestIndex = -1;
        }
        else
        {
            _currentQuestIndex = saveData.QuestIndex-1;
        }
    }
}