using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuilder 
{
    private List<Quest> _quests = new();
    private PlayerMover _mover;
    private PlayerAttacker _playerAttacker;

    public QuestBuilder(PlayerMover mover, PlayerAttacker playerAttacker)
    {
        _mover = mover;
        _playerAttacker = playerAttacker;

        CreateQuests();
    }

    private void CreateQuests()
    {
        CreateMoveQuest();
    }

    public IQuest GetQuest(QuestConfig config)
    {
        foreach (Quest quest in _quests)
        {
            if (quest.GetQuestType() == config.QuestType)
            {
                quest.SetConfig(config);
                return quest;
            }
        }

        return null;
    }

    private void CreateMoveQuest()
    {
        var quest = new MoveQuest(_mover);
        _quests.Add( quest);
    }
}
