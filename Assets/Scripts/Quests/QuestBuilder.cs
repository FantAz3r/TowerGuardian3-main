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

    public QuestBuilder(PlayerMover mover,
        PlayerAttacker playerAttacker,
        Inventory inventory, 
        PlayerCardConfigContainer cardHolder,
        EnemyDetector detector,
        List<Portal> portals)
    {
        _mover = mover;
        _attacker = playerAttacker;
        _inventory = inventory;
        _cardHolder = cardHolder;
        _detector = detector;
        _portals = portals;

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
