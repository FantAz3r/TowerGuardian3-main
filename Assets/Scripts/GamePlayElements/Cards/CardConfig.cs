using System.Collections.Generic;
using UnityEngine;

public abstract class CardConfig : ShopConfig, ICardConfig
{
    [SerializeField, Range(0f, 1f)] private float _chanceToView;
    [SerializeField, Range(0, 5)] private int _level = 0;

    public float ChanceToView => _chanceToView;
    public int Level => _level;

    public abstract CardType GetCardType();
    public abstract List<CardStats> GetStats();

    public CardSaveData CreateSaveData() => new CardSaveData(_level);
    public void InitFromData(CardSaveData data) => _level = data.Level;
    public void Upgrade() => _level++;
}
