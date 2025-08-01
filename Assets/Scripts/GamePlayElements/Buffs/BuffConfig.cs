using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffConfig : ScriptableObject, ICardConfig
{
    [SerializeField] private Image _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _chanceToView;

    public string Name => _name;

    public string Description => _description;

    public Image Icon => _icon;

    public CardType Type => CardType.Buff;

    public float ChanceToView => _chanceToView;

    public Dictionary<string, float> GetStats()
    {
        throw new System.NotImplementedException();
    }
}
