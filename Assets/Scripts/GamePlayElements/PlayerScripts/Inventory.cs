using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private ResourceCollector _collector;
    private Dictionary<ResourceType, int> _resources = new();
    private int _currentAmount = 0;
    private int _startAmount = 0;

    public event Action<Dictionary<ResourceType, int>> ResourceChanged;
    public event Action ResourceAdded;
    public event Action<int> TotalAmountChanged;

    private void Awake()
    {
        _collector = GetComponentInChildren<ResourceCollector>();

        _resources.Add(ResourceType.Coin, _startAmount);
        _resources.Add(ResourceType.Wood, _startAmount);
        _resources.Add(ResourceType.Stone, _startAmount);

        LoadResources();
    }

    private void Start()
    {
        ViewActions();
    }

    private void OnEnable()
    {
        _collector.Collected += Collect;
    }

    private void OnDisable()
    {
        _collector.Collected -= Collect;
    }

    public void Collect(ResourcePiece resource, int amount)
    {
        int spaceLeft = _config.InventoryCapacity - _currentAmount;
        int amountToAdd = Mathf.Min(amount, spaceLeft);

        if (_resources.ContainsKey(resource.PeiceType))
        {
            _resources[resource.PeiceType] += amountToAdd;
        }
        else
        {
            _resources.Add(resource.PeiceType, amountToAdd);
        }

        _currentAmount += amountToAdd;
        ViewActions();
        SaveResources();
    }

    public bool IsEnoughResource(List<CostInfo> costs)
    {
        foreach (var cost in costs)
        {
            if (_resources.ContainsKey(cost.ResourceType) == false || _resources[cost.ResourceType] < cost.Value)
            {
                return false;
            }
        }

        return true;
    }

    public void SpendResource(List<CostInfo> costs)
    {
        foreach (var cost in costs)
        {
            _resources[cost.ResourceType] -= cost.Value;
            _currentAmount -= cost.Value;
        }

        ViewActions();
        SaveResources();
    }

    public bool IsOverflow()
    {
        return _config.InventoryCapacity <= _currentAmount;
    }

    private void ViewActions()
    {
        ResourceAdded?.Invoke();
        ResourceChanged?.Invoke(_resources);
        TotalAmountChanged?.Invoke(_currentAmount);
    }


    private void SaveResources()
    {
        _resources.TryGetValue(ResourceType.Coin, out int coins);
        _resources.TryGetValue(ResourceType.Wood, out int wood);
        _resources.TryGetValue(ResourceType.Stone, out int stones);

        YG2.saves.Coins = Mathf.Max(0, coins);
        YG2.saves.Wood = Mathf.Max(0, wood);
        YG2.saves.Stones = Mathf.Max(0, stones);
        YG2.SaveProgress();
    }

    private void LoadResources()
    {
        if (YG2.saves == null)
            return;

        _resources[ResourceType.Coin] = Mathf.Max(0, YG2.saves.Coins);
        _resources[ResourceType.Wood] = Mathf.Max(0, YG2.saves.Wood);
        _resources[ResourceType.Stone] = Mathf.Max(0, YG2.saves.Stones);

        _currentAmount = 0;
        foreach (var value in _resources.Values)
            _currentAmount += Mathf.Max(0, value);
    }
}
