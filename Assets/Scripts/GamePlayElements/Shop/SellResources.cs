using System.Collections.Generic;
using UnityEngine;

public class SellResources : MonoBehaviour
{
    [SerializeField] private RectTransform parentPanel;
    [SerializeField] private SellResourceView _buttonPrefab;
    [SerializeField] private CounterSlider _slider;
    [SerializeField] private CostResourceData _costResourceData;

    private List<SellResourceView> _productButtons = new();
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = ServiceLocator.Get<IGameFactory>().Player.Inventory;
        _slider.gameObject.SetActive(false);
        
    }

    public void RenderSellItems()
    {
        ClearOldButtons();

        foreach (var config in _costResourceData.PieceConfigs)
        {
            bool interactble = (_inventory.ShowCount(config.Type) > 0);
            SellResourceView button = Instantiate(_buttonPrefab, parentPanel);
            button.Init(_slider);
            button.Render(config, interactble, _inventory.ShowCount(config.Type));
            button.SellRequested += OnSellRequested;
            _productButtons.Add(button);
        }
    }

    private void ClearOldButtons()
    {
        if (_productButtons.Count == 0)
            return;

        foreach (SellResourceView button in _productButtons)
        {
            button.SellRequested -= OnSellRequested;
            Destroy(button.gameObject);
        }

        _productButtons.Clear();
    }

    private void OnSellRequested(SellResourceView button, PieceConfig config, int count)
    {
        List<CostInfo> sellPrice = config.GetSellCosts();

        List<CostInfo> addedResources = new List<CostInfo>
        {
            new CostInfo(config.Type, 1, config.Icon)
        };

        _inventory.AddResousres(MultiplyCost(sellPrice, count));
        _inventory.SpendResource(MultiplyCost(addedResources, count));

        RenderSellItems();
    }

    private List<CostInfo> MultiplyCost(List<CostInfo> sellPrice, int count)
    {
        List<CostInfo> multipliedCost = new List<CostInfo>();

        foreach (CostInfo cost in sellPrice)
        {
            CostInfo multiplied = new CostInfo
            {
                ResourceType = cost.ResourceType,
                Value = cost.Value * count
            };

            multipliedCost.Add(multiplied);
        }

        return multipliedCost;
    }


    private void OnDestroy()
    {
        if (_productButtons.Count == 0)
            return;

        foreach (var button in _productButtons)
        {
            button.SellRequested -= OnSellRequested;
        }
    }
}
