using System.Collections.Generic;
using UnityEngine.UI;
public interface ICardConfig
{
    CardType CardType { get; }
    float ChanceToView { get; }
    string Name { get; }
    string Description { get; }
    Image Icon { get; }

    Dictionary<string, float> GetStats();
}
