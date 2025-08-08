using System.Collections.Generic;
using UnityEngine;
public interface ICardConfig
{
    CardType CardType { get; }
    float ChanceToView { get; }
    string Name { get; }
    string Description { get; }
    Sprite Icon { get; }

    Dictionary<string, float> GetStats();
}
