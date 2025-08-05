using System.Collections.Generic;
using UnityEngine;

public class PlayerCardConfigContainer : MonoBehaviour
{
    private List<ICardConfig> _selectedConfigs = new List<ICardConfig>();
    private List<IStat> _buffedStats;
    public IEnumerable<ICardConfig> SelectedConfigs => _selectedConfigs;

    private void Awake()
    {
        _buffedStats = new List<IStat>(GetComponents<IStat>());
    }

    public void Add(ICardConfig config)
    {
        _selectedConfigs.Add(config);

        if(config is BuffConfig buffConfig)
        {
            foreach (var stat in _buffedStats)
            {
                stat.ApplyBuff(buffConfig.BuffType, buffConfig.IncreaceValue);
            }
        }
    }
}
