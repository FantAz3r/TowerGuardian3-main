using System.Collections.Generic;

public class CurrencyService : ICurrencyService
{
    private Inventory _playerInventory;
    public CurrencyService(Inventory playerInventory)
    {
        _playerInventory = playerInventory;
    }

    public bool CanAfford(List<CostInfo> costs)
    {
       return _playerInventory.IsEnoughResource(costs);
    }

    public void Spend(List<CostInfo> costs)
    {
        _playerInventory.SpendResource(costs);
    }
}
