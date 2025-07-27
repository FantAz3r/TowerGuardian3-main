using System.Collections.Generic;
using UnityEngine.UI;
public interface ICardConfig
{
    CardType Type { get; }
    float ChanceToView { get; }
    string Name { get; }
    string Description { get; }
    Image Icon { get; }

    Dictionary<string, float> GetStats();
}
