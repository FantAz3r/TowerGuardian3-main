using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuffConfig", menuName = "Configs/BuffConfig")]
public class BuffConfig : ScriptableObject, ICardConfig
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField, Range(0f, 1f)] private float _chanceToView;
    [SerializeField] private BuffType _buffType;
    [SerializeField] private float _increaceValue;

    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    public CardType CardType => CardType.Buff;
    public BuffType Type => _buffType;
    public float IncreaceValue => _increaceValue;
    public float ChanceToView => _chanceToView;

    public Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> stats = new Dictionary<string, float>();

        stats.Add(_buffType.ToString(), _increaceValue);

        return stats;
    }
}