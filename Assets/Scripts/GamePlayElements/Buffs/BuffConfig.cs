
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffConfig", menuName = "Configs/BuffConfig")]
public class BuffConfig : CardConfig
{
    [field: SerializeField] public BuffEffectType EffectType { get; private set; }
    [SerializeField] private BuffType _buffType;

    [SerializeField] private float _baseIncreaseValue = 0.2f;
    [SerializeField] private float _upgradeValuePerLevel = 0.1f;

    public BuffType BuffType => _buffType;
    public float IncreaseValue => GetIncreaseValue(Level);

    public float GetIncreaseValue(int level)
    {
        return _baseIncreaseValue + _upgradeValuePerLevel * (level - 1);
    }

    public override List<CardStats> GetStats()
    {
        int level = Level;
        int nextLevel = level + 1;

        return new List<CardStats>
        {
            new CardStats(UIText.IncreaseValue, GetIncreaseValue(level), GetIncreaseValue(nextLevel))
        };
        
    }

    public override CardType GetCardType() => CardType.Buff;
}

