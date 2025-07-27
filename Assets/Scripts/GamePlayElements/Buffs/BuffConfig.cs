using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffConfig : ScriptableObject, IConfig
{
    [SerializeField] private Image _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    public string Name => _name;

    public string Description => _description;

    public Image Icon => _icon;

    public Dictionary<string, float> GetStats()
    {
        throw new System.NotImplementedException();
    }
}
