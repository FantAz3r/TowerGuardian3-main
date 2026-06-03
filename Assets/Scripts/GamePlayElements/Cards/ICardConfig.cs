using System;
using System.Collections.Generic;

public interface ICardConfig : IShopConfig
{
    event Action<ICardConfig> Upgraded;
    float ChanceToView { get; }
    int Level { get; }
    bool HasPlayer { get; }
    bool IsBought { get; }
    int MaxCardLevel { get; }

    CardType GetCardType();
    List<CardStats> GetStats();
    CardSaveData CreateSaveData(bool isBought = false);

    void Upgrade();
    void Regrade();
    void InitFromData(CardSaveData data);
    void SetBought(bool isBought);
    void SetHasPlayer(bool hasPlayer);

    void SetChanceToView(float chance);
}
