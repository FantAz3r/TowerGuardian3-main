using System.Collections.Generic;
using UnityEngine.UI;
public interface IConfig
{
    string Name { get; }
    string Description { get; }
    Image Icon { get; }

    Dictionary<string, float> GetStats();
}
