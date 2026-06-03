using UnityEngine;
public class AllBuffs : AllItems<IBuff, BuffConfig, BuffType>
{
    protected override void Awake()
    {
        base.Awake();
        CreateBuffs();
        SetConfigs();
    }

    protected override BuffType GetTypeFromConfig(BuffConfig config)
    {
        return config.BuffType;
    }

    private void CreateBuffs()
    {
        Items.Add(new MaxHealthBuff(Player.Health));
        Items.Add(new SpeedBuff(Player.Mover));
        Items.Add(new CollectRangeBuff(Player.ResourceCollector));
        Items.Add(new RegenerationBuff(Player.HealthRegeneration));
    }

    private void SetConfigs()
    {
        CardData cardData = Resources.Load<CardData>(GameConstants.CardData);

        foreach (var buff in Items)
        {
            foreach (var config in cardData.BuffConfigs)
            {
                if (buff.Type == config.BuffType)
                {
                    buff.SetConfig(config);
                }
            }
        }
    }
}
