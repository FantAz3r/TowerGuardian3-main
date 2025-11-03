using System.Collections.Generic;

public class QuestBuilder 
{
    private List<Quest> _quests = new();
    private PlayerMover _mover;
    private PlayerAttacker _attacker;
    private Inventory _inventory;
    private PlayerCardConfigContainer _cardHolder;
    private Shop _shop;

    public QuestBuilder(PlayerMover mover, PlayerAttacker playerAttacker, Inventory inventory, PlayerCardConfigContainer cardHolder, Shop shop)
    {
        _mover = mover;
        _attacker = playerAttacker;
        _inventory = inventory;
        _cardHolder = cardHolder;
        _shop = shop;

        CreateQuests();
    }

    private void CreateQuests()
    {
        _quests.Add(new MoveQuest(_mover));
        _quests.Add(new AttackQuest(_attacker));
        _quests.Add(new CollectQuest(_inventory));
        _quests.Add(new BuyWeaponQuest(_shop));
    }

    public IQuest GetQuest(QuestConfig config)
    {
        foreach (Quest quest in _quests)
        {
            if (quest.GetQuestType() == config.QuestType)
            {
                quest.SetConfig(config);

                if(quest is IUpdatableQuest)
                {
                    (quest as IUpdatableQuest).Set(config.TargetValue);
                }

                return quest;
            }
        }

        return null;
    }
}
