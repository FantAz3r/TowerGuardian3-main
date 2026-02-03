using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class Tutorial : MonoBehaviour
{
    private List<IQuest> _quests = new();

    private QuestBuilder _builder;
    private int _currentQuestIndex = -1;
    private LevelID _level;
    private IQuest _currentQuest;
    private bool _isTutorialComplete = false;

    public event Action AllQuestsCompleted, QuestCompleted;
    public event Action<IQuest> QuestStarted;

    public void Init(QuestBuilder builder, LevelID level, IReadOnlyList<QuestType> questsForThisLevel)
    {
        _level = level;
        _builder = builder;
        QuestData questData = Resources.Load<QuestData>(GameConstants.QuestData);

        foreach (var questInfo in questData.QuestInfos)
        {
            if (questsForThisLevel.Contains(questInfo.Type))
            {
                _quests.Add(_builder.GetQuest(questInfo.Config));
            }
        }

        LoadQuestProgress();
    }

    public void RunQuests()
    {
        SwitchQuest();
    }

    private void OnDestroy()
    {
        SaveQuestProgress();
    }

    private void SwitchQuest()
    {
        if (_currentQuest != null)
        {
            _currentQuest.OnCompleted -= OnQuestCompleted;
            _currentQuest.Stop();
        }

        _currentQuestIndex++;

        if (_currentQuestIndex >= _quests.Count)
        {
            _currentQuest = null;
            AllQuestsCompleted?.Invoke();
            return;
        }

        _currentQuest = _quests[_currentQuestIndex];
        _currentQuest.OnCompleted += OnQuestCompleted;
        _currentQuest.Run();
        QuestStarted?.Invoke(_currentQuest);
    }

    private void OnQuestCompleted()
    {
        QuestCompleted?.Invoke();
        SwitchQuest();
    }

    private void SaveQuestProgress()
    {
        if (YG2.saves.QuestProgress == null)
            YG2.saves.QuestProgress = new List<QuestSaveData>();

        int index = YG2.saves.QuestProgress.FindIndex(q => q.Level == _level);

        if (_isTutorialComplete == false)
        {
            var saveData = new QuestSaveData(_level, 0,0, _currentQuestIndex);
            if (index >= 0)
                YG2.saves.QuestProgress[index] = saveData;
            else
                YG2.saves.QuestProgress.Add(saveData);
        }
        else
        {
            var saveData = new QuestSaveData(_level, 0, 0, 0);
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
            _currentQuestIndex = saveData.QuestIndex - 1;
        }
    }

    private void ResetProgress()
    {
        _currentQuestIndex = 0;
    }
}