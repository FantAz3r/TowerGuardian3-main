using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigContainer : MonoBehaviour
{
    private List<ICardConfig> _selectedConfigs = new List<ICardConfig>();

    public IEnumerable<ICardConfig> SelectedConfigs => _selectedConfigs;

    public void Add(ICardConfig config)
    {
        _selectedConfigs.Add(config);
    }
}
