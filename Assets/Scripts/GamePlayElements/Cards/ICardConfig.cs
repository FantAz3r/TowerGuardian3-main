using System;
using System.Collections.Generic;

public interface ICardConfig : IShopConfig
{
    float ChanceToView { get; }
    int Level { get; }
    bool HasPlayer { get; }
    bool IsBought { get; }

    event Action<ICardConfig> Upgraded;

    CardType GetCardType();
    List<CardStats> GetStats();
    CardSaveData CreateSaveData(bool isBought = false);

    void Upgrade();
    void InitFromData(CardSaveData data);
    void SetBought(bool isBought);
    void SetHasPlayer(bool hasPlayer);
}
