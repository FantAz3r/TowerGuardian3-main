using UnityEngine;

public class SelectWeaponCardQuest : Quest
{
    private WeaponPanel _panel;
    private CardData _data;

    public SelectWeaponCardQuest()
    {
        _data = Resources.Load<CardData>(GameConstants.CardData);
        SetWeaponCardChance(0);
    }

    public override QuestType GetQuestType() => QuestType.SelectWeaponCard;

    public override void Run()
    {
        _panel = ServiceLocator.Get<IUIFactory>().HUD.WeaponPanel;

        base.Run();
        SetWeaponCardChance(1);
        _panel.WeaponAdded += Complete;
    }

    public override void Stop()
    {
        _panel.WeaponAdded -= Complete;
        base.Stop();
        SetWeaponCardChance(0);
    }

    public override void Complete()
    {
        _panel.WeaponAdded -= Complete;
        SetWeaponCardChance(0);
        base.Complete();
    }

    private void SetWeaponCardChance(float chance)
    {
        foreach (var card in _data.GetConfigs())
        {
            if (card.GetCardType() == CardType.Weapon)
            {
                card.SetChanceToView(chance);
            }
        }
    }
}
