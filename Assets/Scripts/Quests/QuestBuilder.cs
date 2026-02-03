using System;
using System.Collections.Generic;
public class QuestBuilder
{
    private List<IQuest> _quests = new();
    private Player _player;
    private List<Portal> _portals;
    private TowerDoor _door;
    private StairsTrigger _stairsTrigger;

    public QuestBuilder(Player player,
        List<Portal> portals = null,
        TowerDoor door = null,
        StairsTrigger stairsTrigger = null)
    {
        _player = player;
        _stairsTrigger = stairsTrigger;
        _portals = portals;
        _door = door;

        CreateQuests();
    }

    private void CreateQuests()
    {
        _quests.Add(new MoveQuest(_player.PlayerMover));
        _quests.Add(new AttackQuest(_player.Attacker));
        _quests.Add(new CollectQuest(_player.Inventory));
        _quests.Add(new UpgradeQuest(_player));
        _quests.Add(new KillQuest(_player.Detector));
        _quests.Add(new ExitLevelQuest(_portals));
        _quests.Add(new EnterTowerQuest(_door));
        _quests.Add(new UpstairsQuest(_stairsTrigger));
        _quests.Add(new EnterFirstLevelQuest(GetPortalByLevel(LevelID.Level1)));
        _quests.Add(new EnterSecondLevelQuest(GetPortalByLevel(LevelID.Level2)));
        _quests.Add(new DefendPortalQuest(_portals));
    }

    private Portal GetPortalByLevel(LevelID level)
    {
        foreach (var portal in _portals)
        {
            if (portal.NextLevel == level)
            {
                return portal;
            }
        }

        return null;
    }

    public IQuest GetQuest(QuestConfig config)
    {
        foreach (IQuest quest in _quests)
        {
            if (quest.GetQuestType() == config.QuestType)
            {
                quest.SetConfig(config);
                return quest;
            }
        }

        throw new ArgumentNullException();
    }
}
