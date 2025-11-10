using System;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour 
{
    [SerializeField] private List<QuestConfig> _configs;
    private List<IQuest> _quests = new();

    private QuestBuilder _builder;
    private int _currentQuestIndex = -1;
    private IQuest _currentQuest;
    private bool _isTutorialComplete = false;

    public event Action<Sprite, string> QuestSeted;
    public event Action<string> QuestUpdated;
    public event Action Complited;

    public void Init(QuestBuilder builder)
    {
        _builder = builder;

        foreach (var config in _configs)
        {
            IQuest quest = _builder.GetQuest(config);

            if (quest != null)
            {
                _quests.Add(quest);
            }
        }
    }

    public void RunNextQuest()
    {
        if (_currentQuestIndex > 0)
        {
            _quests[_currentQuestIndex].Stop();
            _quests[_currentQuestIndex].OnCompleted -= OnQuestCompleted;
        }

        _currentQuestIndex++;

        if (_currentQuestIndex >= _quests.Count)
        {
            _isTutorialComplete = true;
            Complited?.Invoke();
            Debug.Log("Туториал завершён!");
            return;
        }
        
        _currentQuest = _quests[_currentQuestIndex];
        _currentQuest.OnCompleted += OnQuestCompleted;
        _currentQuest.Run();
        QuestSeted?.Invoke(_currentQuest.Config.Image, _currentQuest.Config.Description);

        if (_currentQuest is IUpdatableQuest)
        {
            (_currentQuest as IUpdatableQuest).Updated += OnQuestUpdated;
        }
    }

    private void OnQuestUpdated(int value)
    {
        string updatebleDescription = $"{_currentQuest.Config.Description} {value}/ {(_currentQuest as IUpdatableQuest).Goal}";
        QuestUpdated?.Invoke(updatebleDescription);
    }

    private void OnQuestCompleted()
    {
        if (_currentQuest is IUpdatableQuest)
        {
            (_currentQuest as IUpdatableQuest).Updated -= OnQuestUpdated;
        }

        RunNextQuest();
    }
}
    