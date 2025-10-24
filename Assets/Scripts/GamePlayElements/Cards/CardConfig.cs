using System.Collections.Generic;
using UnityEngine;

public abstract class CardConfig : ScriptableObject, ICardConfig, IShopConfig
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField, Range(0f, 1f)] private float _chanceToView;
    [Range(0, 5)] private int _level = 0;
    [SerializeField] private List<CostInfo> _costs = new List<CostInfo>();

    public float ChanceToView => _chanceToView;

    public string Name => _name;

    public string Description => _description;

    public Sprite Icon => _icon;

    public int Level => _level;

    public abstract CardType GetCardType();
    public abstract List<CardStats> GetStats();

    public CardSaveData CreateSaveData()
    {
        return new CardSaveData(_level);
    }

    public void InitFromData(CardSaveData data)
    {
        _level = data.Level;
    }

    public void Upgrade()
    {
        _level++;
    }

    public List<CostInfo> GetCosts() => _costs;
}
