using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityConfig : CardConfig
{
    [SerializeField] private AbilityType _abilityType;

    public AbilityType Type => _abilityType;

    public override CardType GetCardType()
    {
        return CardType.Ability;
    }
}

