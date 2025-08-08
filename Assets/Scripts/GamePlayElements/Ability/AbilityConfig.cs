using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityConfig : ScriptableObject, ICardConfig
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _chanceToView;
    [SerializeField] private AbilityType _abilityType;

    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    public CardType CardType => CardType.Ability;
    public AbilityType Type => _abilityType;
    public float ChanceToView => _chanceToView;

    public abstract Dictionary<string, float> GetStats();
}

