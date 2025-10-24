using System.Collections.Generic;

public interface ICurrencyService
{
    bool CanAfford(List<CostInfo> costs);
    void Spend(List<CostInfo> costs);
}