using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private RectTransform _cardContentParent;
    [SerializeField] private RectTransform _buildingsContentParent;
    [SerializeField] private ProductViewer _productButtonPrefab;

    [SerializeField] private RectTransform _cards;
    [SerializeField] private RectTransform _buildings;

    [SerializeField] private List<ShopConfig> _allConfigs;

    private List<IShopConfig> _shopConfigs = new();
    private List<ProductViewer> _productButtons = new();
    private Inventory _playerInventory;
    private AllCardConfigs _allCardConfigs;

    public event Action WeaponAdded;

    private void Awake()
    {
        RectTransform parent;

        foreach (var config in _allConfigs)
        {
            _shopConfigs.Add(config);
        }

        foreach (var config in _shopConfigs)
        {
            if (config is CardConfig)
            {
                parent = _cardContentParent;
            }
            else
            {
                parent = _buildingsContentParent;
            }

            var button = Instantiate(_productButtonPrefab, parent);
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
        _cards.gameObject.SetActive(false);
        _buildings.gameObject.SetActive(false);
        gameObject.SetActive(false);
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
        if (CanAfford(config) == false)
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

        if (config is WeaponConfig)
        {
            WeaponAdded?.Invoke();
        }
    }
}

