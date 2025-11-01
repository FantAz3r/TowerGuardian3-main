using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tutorial : MonoBehaviour 
{
    [SerializeField] private List<QuestConfig> _configs;
    private List<IQuest> _quests;

    private QuestBuilder _builder;
    private int _currentQuestIndex = 0;
    private IQuest _currentQuest;
    private bool _isTutorialComplete = false;

    public event Action<Sprite, string> QuestSeted;
    public event Action<string> QuestUpdated;

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

        if (_quests.Count == 0)
        {
            Debug.LogWarning("Список квестов пуст!");
            _isTutorialComplete = true;
            return;
        }

        RunNextQuest();
    }

    private void RunNextQuest()
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
            Debug.Log("Туториал завершён!");
            return;
        }

        _currentQuest = _quests[_currentQuestIndex];
        _currentQuest.OnCompleted += OnQuestCompleted;
        _currentQuest.Run();
        QuestSeted?.Invoke(_currentQuest.Sprite, _currentQuest.Description);

        if (_currentQuest is IUpdatebleQuest)
        {
            (_currentQuest as IUpdatebleQuest).Updated += OnQuestUpdated;
        }
    }

    private void OnQuestUpdated(int value)
    {
        string updatebleDescription = $"{_currentQuest.Description} {value}/ {(_currentQuest as IUpdatebleQuest).Goal}";
        QuestUpdated?.Invoke(updatebleDescription);
    }

    private void OnQuestCompleted()
    {
        if (_currentQuest is IUpdatebleQuest)
        {
            (_currentQuest as IUpdatebleQuest).Updated -= OnQuestUpdated;
        }

        RunNextQuest();
    }
}
    