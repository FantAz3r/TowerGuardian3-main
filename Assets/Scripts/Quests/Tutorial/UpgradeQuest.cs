
using UnityEngine;

public class UpgradeQuest : Quest
{
    private PlayerCardConfigContainer _cardContainer;
    public override QuestType GetQuestType()
    {
        return QuestType.Upgrade;
    }

    public UpgradeQuest(PlayerCardConfigContainer cardContainer)
    {
        _cardContainer = cardContainer;
    }

    public override void Run()
    {
        _cardContainer.Added += Complete;
        _cardContainer.Upgraded += Complite;
    }

    public override void Complete()
    {
        _cardContainer.Added -= Complete;
        _cardContainer.Upgraded += Complite;
        base.Complete();
    }

    private void Complite(ICardConfig useles)
    {
        Complete();
    }
}
