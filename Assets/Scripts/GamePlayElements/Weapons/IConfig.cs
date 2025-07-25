using System.Collections.Generic;
using UnityEngine;
public interface IConfig
{
    string Name { get; }
    string Description { get; }
    Sprite Icon { get; }

    Dictionary<string, float> GetStats();
}
