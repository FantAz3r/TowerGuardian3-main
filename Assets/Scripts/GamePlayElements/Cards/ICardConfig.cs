using System.Collections.Generic;

public interface ICardConfig : IShopConfig
{
    float ChanceToView { get; }
    int Level { get; }
    CardType GetCardType();
    List<CardStats> GetStats();
    CardSaveData CreateSaveData();
    void Upgrade();
    void InitFromData(CardSaveData data);
}
