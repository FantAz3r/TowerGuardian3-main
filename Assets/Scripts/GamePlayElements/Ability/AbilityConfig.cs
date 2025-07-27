using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityConfig : ScriptableObject, ICardConfig
{
    [SerializeField] private Image _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _chanceToView;

    public string Name => _name;

    public string Description => _description;

    public Image Icon => _icon;

    public CardType Type => CardType.Ability;

    public float ChanceToView => _chanceToView;

    public Dictionary<string, float> GetStats()
    {
        throw new System.NotImplementedException();
    }
}
