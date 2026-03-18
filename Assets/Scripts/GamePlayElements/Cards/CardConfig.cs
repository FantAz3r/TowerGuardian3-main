using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardConfig : ShopConfig, ICardConfig
{
    [field: SerializeField, Range(0f, 1f)] public float ChanceToView { get; private set; }
    [field: SerializeField] public int Level { get; private set; } = 0;
    [field: SerializeField] public bool HasPlayer { get; private set; }
    [field: SerializeField] public bool IsBought { get; private set; }

    public event Action<ICardConfig> Upgraded;

    public abstract CardType GetCardType();
    public abstract List<CardStats> GetStats();

    public CardSaveData CreateSaveData(bool isBought = false) => new CardSaveData(Level, ID, isBought, HasPlayer);

    public void InitFromData(CardSaveData data)
    {
        Level = data.Level;
        HasPlayer = data.HasPlayer;
        IsBought = data.IsBought;
    }

    public void Upgrade()
    {
        Level++;
        Upgraded?.Invoke(this);
    }

    public void Regrade()
    {
        if (Level <= 0) return;

        Level--;
        Upgraded?.Invoke(this);
    }

    public override List<CostInfo> GetCosts()
    {
        List<CostInfo> increasedCosts = new();
        float exponent = 1.5f;

        foreach (var info in Costs)
        {
            float newAmount = info.Value * Mathf.Pow(Level == 0 ? 1 : Level, exponent);
            increasedCosts.Add(new CostInfo(info.ResourceType, Mathf.CeilToInt(newAmount), info.Image));
        }

        return increasedCosts;
    }

    public override List<CostInfo> GetSellCosts()
    {
        List<CostInfo> sellCosts = new List<CostInfo>();
        float sellCoefficient = 0.5f;

        foreach (var info in GetCosts())
        {
            float newAmount = info.Value * sellCoefficient;
            sellCosts.Add(new CostInfo(info.ResourceType, Mathf.CeilToInt(newAmount), info.Image));
        }

        return sellCosts;
    }

    public void SetBought(bool isBought)
    {
        IsBought = isBought;
    }

    public void SetHasPlayer(bool hasPlayer)
    {
        HasPlayer = hasPlayer;
    }

    public void SetChanceToView(float chance)
    {
        ChanceToView = Mathf.Clamp01(chance);
    }
}

