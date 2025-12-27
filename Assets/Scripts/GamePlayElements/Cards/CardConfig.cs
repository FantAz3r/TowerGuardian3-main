using System.Collections.Generic;
using UnityEngine;

public abstract class CardConfig : ShopConfig, ICardConfig
{
    [SerializeField, Range(0f, 1f)] private float _chanceToView;
    [SerializeField] private int _level = 0;
    [SerializeField] private bool _hasPlayer;
    [SerializeField] private bool _isBought;

    public float ChanceToView => _chanceToView;
    public int Level => _level;
    public bool HasPlayer => _hasPlayer;
    public bool IsBought => _isBought;

    public abstract CardType GetCardType();
    public abstract List<CardStats> GetStats();

    public CardSaveData CreateSaveData(bool isBought = false) => new CardSaveData(_level, ID, isBought, _hasPlayer);

    public void InitFromData(CardSaveData data)
    {
        _level = data.Level;
        _hasPlayer = data.HasPlayer;
        _isBought = data.IsBought;
    } 

    public void Upgrade() => _level++;

    public override List<CostInfo> GetCosts()
    {
        List<CostInfo> increasedCosts = new();
        float exponent = 1.5f; 

        foreach (var info in Costs)
        {
            float newAmount = info.Value * Mathf.Pow(_level == 0 ? 1 : _level, exponent);
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
            sellCosts.Add(new CostInfo(info.ResourceType, Mathf.CeilToInt(newAmount)));
        }

        return sellCosts;
    }

    public void SetBought(bool isBought)
    {
        _isBought = isBought;
    }
}
