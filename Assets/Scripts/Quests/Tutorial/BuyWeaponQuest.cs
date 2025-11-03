
public class BuyWeaponQuest : Quest
{
    private Shop _shop;

    public BuyWeaponQuest(Shop shop)
    {
        _shop = shop;
    }

    public override void Run()
    {
        _shop.WeaponAdded += Complete;
    }

    public override QuestType GetQuestType()
    {
        return QuestType.BuyWeapon;
    }

    public void Complete(ICardConfig config)
    {
        _shop.WeaponAdded -= Complete;
        CompleteQuest();
    }
}
