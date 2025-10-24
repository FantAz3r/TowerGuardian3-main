using System.Collections.Generic;
using UnityEngine;
public interface ICardConfig 
{
    float ChanceToView { get; }
    string Name { get; }
    string Description { get; }
    Sprite Icon { get; }
    int Level { get; }

    CardType GetCardType();
    List<CardStats> GetStats();
    CardSaveData CreateSaveData();
    void Upgrade();
    void InitFromData(CardSaveData data);
}
