using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using YG;

public class Tutorial : MonoBehaviour
{
    private List<QuestConfig> _questConfigs = new();
    private List<IQuest> _quests = new();

    private QuestBuilder _builder;
    private int _currentQuestIndex = -1;
    private LevelID _level;
    private IQuest _currentQuest;
    private bool _isTutorialComplete = false;
    private int _valueProgress;

    public event Action<IQuest> QuestSeted;
    public event Action<string> QuestUpdated;
    public event Action Complited;
    public event Action CompliteWithoutLust;

    public void Init(QuestBuilder builder, QuestData questData, LevelID level, IReadOnlyList<QuestType> questsForThisLevel)
    {
        _level = level;
        _builder = builder;

        foreach (var questInfo in questData.QuestInfos)
        {
            if (questsForThisLevel.Contains(questInfo.Type))
            {
                _questConfigs.Add(questInfo.Config);
                _quests.Add(_builder.GetQuest(questInfo.Config));
            }
        }

        YG2.onSwitchLang += OnChangeLang;
    }

    public void RunNextQuest()
    {
        if (_currentQuestIndex > 0)
        {
            _quests[_currentQuestIndex].Stop();
            _quests[_currentQuestIndex].OnCompleted -= OnQuestCompleted;
        }

        _currentQuestIndex++;

        if (_currentQuestIndex >= _quests.Count - 1)
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
        QuestSeted?.Invoke(_currentQuest);

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

    private void OnChangeLang(string useles)
    {
        int minGoalForView = 2;
        QuestSeted?.Invoke(_currentQuest);

        if (_currentQuest.Goal >= minGoalForView)
        {
            string updatebleDescription = $"{_currentQuest.Config.Description} {_valueProgress}/ {_currentQuest.Goal}";
            QuestUpdated?.Invoke(updatebleDescription);
        }
    }

    private void SaveQuestProgress()
    {
        if (YG2.saves.QuestProgress == null)
            YG2.saves.QuestProgress = new List<QuestSaveData>();

        int index = YG2.saves.QuestProgress.FindIndex(q => q.Level == _level);

        if (_isTutorialComplete == false)
        {
            var saveData = new QuestSaveData(_level, _valueProgress, _currentQuestIndex);
            if (index >= 0)
                YG2.saves.QuestProgress[index] = saveData;
            else
                YG2.saves.QuestProgress.Add(saveData);
        }
        else
        {
            var saveData = new QuestSaveData(_level, 0, 0);
            if (index >= 0)
                YG2.saves.QuestProgress[index] = saveData;
            else
                YG2.saves.QuestProgress.Add(saveData);
        }

        YG2.SaveProgress();
    }

    private void LoadQuestProgress()
    {
        if (YG2.saves.QuestProgress == null)
        {
            ResetProgress();
            return;
        }

        var saveData = YG2.saves.QuestProgress.Find(quest => quest.Level == _level);

        if (saveData.Level == LevelID.None)
        {
            ResetProgress();
        }
        else
        {
            _currentQuestIndex = saveData.QuestID - 1;
        }
    }

    private void ResetProgress()
    {
        _currentQuestIndex = 0;
        _valueProgress = 0;
    }

    private void UpdateProgress(QuestSaveData saveData, int index)
    {
        if (index >= 0)
            YG2.saves.QuestProgress[index] = saveData;
        else
            YG2.saves.QuestProgress.Add(saveData);
    }

}