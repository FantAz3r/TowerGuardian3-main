using System.Collections.Generic;

public interface ICardConfig : IShopConfig
{
    float ChanceToView { get; }
    int Level { get; }
    bool HasPlayer { get; }
    bool IsBought { get; }

    CardType GetCardType();
    List<CardStats> GetStats();
    CardSaveData CreateSaveData(bool isBought = false);

    void Upgrade();
    void InitFromData(CardSaveData data);
}
