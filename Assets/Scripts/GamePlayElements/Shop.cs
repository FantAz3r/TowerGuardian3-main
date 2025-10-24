using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private ProductViewer _productButtonPrefab;
    [SerializeField] private List<ScriptableObject> _allConfigs;

    private List<IShopConfig> _shopConfigs = new();
    private List<ProductViewer> _productButtons = new();
    private Inventory _playerInventory;
    private AllCardConfigs _allCardConfigs;

    private void Awake()
    {
        foreach (var config in _allConfigs)
        {
            if (config is IShopConfig)
            {
                _shopConfigs.Add(config as IShopConfig);
            }
        }

        foreach (var config in _shopConfigs)
        {
            var button = Instantiate(_productButtonPrefab, _contentParent);
            button.BuyRequested += OnBuyRequested;
            _productButtons.Add(button);
        }
    }

    public void Init(Inventory playerInventory, AllCardConfigs cardConfigs)
    {
        _playerInventory = playerInventory;
        _allCardConfigs = cardConfigs;
        _playerInventory.ResourceAdded += RenderAll;

        RenderAll();  
    }

    private void OnDestroy()
    {
        if (_playerInventory != null)
            _playerInventory.ResourceAdded -= RenderAll;

        foreach (var button in _productButtons)
        {
            button.BuyRequested -= OnBuyRequested;
        }
    }

    private void RenderAll()
    {
        for (int i = 0; i < _shopConfigs.Count; i++)
        {
            bool canBuy = CanAfford(_shopConfigs[i]);
            _productButtons[i].Render(_shopConfigs[i], canBuy);
            _productButtons[i].gameObject.SetActive(true);
        }
    }

    private bool CanAfford(IShopConfig config)
    {
        if (_playerInventory == null)
            return true;

        return _playerInventory.IsEnoughResource(config.GetCosts());
    }

    private void OnBuyRequested(ProductViewer button, IShopConfig config)
    {
        if (!CanAfford(config))
        {
            Debug.Log("Не хватает ресурсов");
            return;
        }

        _playerInventory?.SpendResource(config.GetCosts());
        Define(config);
        RenderAll();  
    }

    private void Define(IShopConfig config)
    {
        if (config is ICardConfig)
        {
            _allCardConfigs.Add(config as ICardConfig);
        }
    }
}
