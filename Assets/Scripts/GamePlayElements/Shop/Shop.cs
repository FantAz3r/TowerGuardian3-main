using System.Collections.Generic;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private RectTransform _weaponContentParent;
    [SerializeField] private RectTransform _abilitiesContentParent;
    [SerializeField] private RectTransform _buffContentParent;

    [SerializeField] private ProductViewer _productButtonPrefab;
    [SerializeField] private CardData _cardData;

    private List<ICardConfig> _shopConfigs = new();
    private List<ProductViewer> _productButtons = new();
    private Inventory _playerInventory;
    private ITimeService _timeService;

    private void OnDestroy()
    {
        foreach (var button in _productButtons)
        {
            button.BuyRequested -= OnBuyRequested;
        }
    }

    private void OnDisable()
    {
        _timeService.Resume();
    }

    public void Init(Inventory playerInventory)
    {
        _timeService = ServicesLocator.GetService<ITimeService>();
        _playerInventory = playerInventory;
        gameObject.SetActive(false);
    }

    public void OnActivate()
    {
        gameObject.SetActive(true);

        LoadContent();
        RenderAll();

        _weaponContentParent.gameObject.SetActive(true);
        _abilitiesContentParent.gameObject.SetActive(false);
        _buffContentParent.gameObject.SetActive(false);
    }

    private void LoadContent()
    {
        ClearOldButtons();

        RectTransform parent;

        foreach (var config in _cardData.GetConfigs())
        {
            _shopConfigs.Add(config);
        }

        LoadCards();

        foreach (var config in _shopConfigs)
        {
            if (config is WeaponConfig)
            {
                parent = _weaponContentParent;
            }
            else if (config is AbilityConfig)
            {
                parent = _abilitiesContentParent;
            }
            else if(config is BuffConfig)
            {
                parent = _buffContentParent;
            }
            else
            {
                parent = null;
                Debug.Log("конфиг " + config + " не подходит в магазин");
            }

            var button = Instantiate(_productButtonPrefab, parent);
            button.BuyRequested += OnBuyRequested;
            _productButtons.Add(button);
        }
    }

    private void RenderAll()
    {
        for (int i = 0; i < _shopConfigs.Count; i++)
        {
            bool canBuy = CanAfford(_shopConfigs[i]);
            _productButtons[i].gameObject.SetActive(true);
            _productButtons[i].Render(_shopConfigs[i], true, canBuy);
        }
    }

    private void ClearOldButtons()
    {
        foreach (ProductViewer button in _productButtons)
        {
            button.BuyRequested -= OnBuyRequested;
            Destroy(button.gameObject);
        }

        _shopConfigs.Clear();
        _productButtons.Clear();
    }

    private bool CanAfford(IShopConfig config)
    {
        if (_playerInventory == null)
            return true;

        return _playerInventory.IsEnoughResource(config.GetCosts());
    }

    private void OnBuyRequested(ProductViewer button, ICardConfig config)
    {
        if (CanAfford(config) == false)
        {
            Debug.Log("Не хватает ресурсов");
            return;
        }

        _playerInventory?.SpendResource(config.GetCosts());

        config.Upgrade();
        UpdateCardSave(config);

        LoadContent();
        RenderAll();
    }

    private void LoadCards()
    {
        if (YG2.saves.AllCards == null)
        {
            foreach( var card in _cardData.GetConfigs())
            {
                CardSaveData cardData = new CardSaveData(0, card.ID, false, false);
                card.InitFromData(cardData);
            }

            return;
        }

        foreach (var card in _shopConfigs)
        {
            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);
            card.InitFromData(cardData);
        }
    }

    private void UpdateCardSave(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            YG2.saves.AllCards = new();

        YG2.saves.AllCards.RemoveAll(savedCard => savedCard.ID == card.ID);
        YG2.saves.AllCards.Add(card.CreateSaveData(true));
        YG2.SaveProgress();
    }
}

