using System.Collections.Generic;
public class QuestBuilder
{
    private List<Quest> _quests = new();
    private PlayerMover _mover;
    private PlayerAttacker _attacker;
    private Inventory _inventory;
    private PlayerCardConfigContainer _cardHolder;
    private EnemyDetector _detector;
    private List<Portal> _portals;
    private TowerDoor _door;
    private StairsTrigger _stairsTrigger;

    public QuestBuilder(PlayerMover mover,
        PlayerAttacker playerAttacker,
        Inventory inventory,
        PlayerCardConfigContainer cardHolder,
        EnemyDetector detector,
        List<Portal> portals,
        TowerDoor door,
        StairsTrigger stairsTrigger)
    {
        _stairsTrigger = stairsTrigger;
        _mover = mover;
        _attacker = playerAttacker;
        _inventory = inventory;
        _cardHolder = cardHolder;
        _detector = detector;
        _portals = portals;
        _door = door;

        CreateQuests();
    }

    private void CreateQuests()
    {
        _quests.Add(new MoveQuest(_mover));
        _quests.Add(new AttackQuest(_attacker));
        _quests.Add(new CollectQuest(_inventory));
        _quests.Add(new UpgradeQuest(_cardHolder));
        _quests.Add(new KillQuest(_detector));
        _quests.Add(new ExitLevelQuest(_portals));
        _quests.Add(new EnterTowerQuest(_door));
        _quests.Add(new UpstairsQuest(_stairsTrigger));
        _quests.Add(new EnterFirstLevelQuest(GetPortalByLevel(LevelID.Level1)));
        _quests.Add(new EnterFirstLevelQuest(GetPortalByLevel(LevelID.Level2)));
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
}
