using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class Tutorial : MonoBehaviour 
{
    private List<QuestConfig> _questConfigs = new();
    private List<IQuest> _quests = new();

    private QuestBuilder _builder;
    private int _currentQuestIndex = -1;
    private IQuest _currentQuest;
    private bool _isTutorialComplete = false;
    private int _valueProgress;

    public event Action<Sprite, string> QuestSeted;
    public event Action<string> QuestUpdated;
    public event Action Complited;
    public event Action CompliteWithoutLust;

    public void Init(QuestBuilder builder, QuestData questData, IReadOnlyList<QuestType> questsForThisLevel)
    {
        foreach(var questInfo in questData.QuestInfos)
        {
            if(questsForThisLevel.Contains(questInfo.Type))
            {
                _questConfigs.Add(questInfo.Config);
            }
        }

        _builder = builder;

        foreach (var config in _questConfigs)
        {
            IQuest quest = _builder.GetQuest(config);

            if (quest != null)
            {
                _quests.Add(quest);
            }
        }

        YG2.onCorrectLang += On—hangeLang;
    }

    public void RunNextQuest()
    {
        if (_currentQuestIndex > 0)
        {
            _quests[_currentQuestIndex].Stop();
            _quests[_currentQuestIndex].OnCompleted -= OnQuestCompleted;
        }

        _currentQuestIndex++;

        if(_currentQuestIndex >= _quests.Count-1)
        {
            CompliteWithoutLust?.Invoke();
        }

        if (_currentQuestIndex >= _quests.Count)
        {
            _isTutorialComplete = true;
            Complited?.Invoke();
            return;
        }
        
        _currentQuest = _quests[_currentQuestIndex];
        _currentQuest.OnCompleted += OnQuestCompleted;
        _currentQuest.Run();
        QuestSeted?.Invoke(_currentQuest.Config.Image, _currentQuest.Config.Description);

        _currentQuest.Updated += OnQuestUpdated;
    }

    private void OnQuestUpdated(int value)
    {
        _valueProgress = value;
        string updatebleDescription = $"{_currentQuest.Config.Description} {_valueProgress}/ {_currentQuest.Goal}";
        QuestUpdated?.Invoke(updatebleDescription);
    }

    private void OnQuestCompleted()
    {
        _currentQuest.Updated -= OnQuestUpdated;
        RunNextQuest();
    }

    private void On—hangeLang(string useles)
    {
        QuestSeted?.Invoke(_currentQuest.Config.Image, _currentQuest.Config.Description);

        string updatebleDescription = $"{_currentQuest.Config.Description} {_valueProgress}/ {_currentQuest.Goal}";
        QuestUpdated?.Invoke(updatebleDescription);
    }
}
    