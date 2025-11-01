using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuilder 
{
    private List<Quest> _quests = new();
    private AllQuests _allQuests;
    private PlayerMover _mover;
    private PlayerAttacker _playerAttacker;

    public QuestBuilder(PlayerMover mover, PlayerAttacker playerAttacker)
    {
        _mover = mover;
        _playerAttacker = playerAttacker;

        CreateQuests();
    }


    public void CreateQuests()
    {
        CreateMoveQuest();
    }

    public IQuest GetQuest(QuestConfig config)
    {
        foreach (Quest quest in _quests)
        {
            if (quest.QuestType == config.QuestType)
                return quest;
        }

        return null;
    }

    private void CreateMoveQuest()
    {
        var quest = new MoveQuest();
        quest.Init(_mover);
        _quests.Add( quest);
    }
}
