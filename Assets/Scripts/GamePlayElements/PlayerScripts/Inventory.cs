using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private ResourceCollector _collector;
    private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
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
    }

    public bool IsEnoughResource(Dictionary<ResourceType, int> cost)
    {
        foreach (var pair in cost)
        {
            if (_resources.ContainsKey(pair.Key) == false || _resources[pair.Key] < pair.Value)
            {
                return false;
            }
        }

        return true;
    }

    public void SpendResource(Dictionary<ResourceType, int> cost)
    {
        foreach (var pair in cost)
        {
            _resources[pair.Key] -= pair.Value;
            _currentAmount -= pair.Value;
        }

        ViewActions();
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
}
