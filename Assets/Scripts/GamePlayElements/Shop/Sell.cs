using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Sell : MonoBehaviour
{
    [SerializeField] private RectTransform _weaponParent;
    [SerializeField] private RectTransform _abilityParent;
    [SerializeField] private RectTransform _buffParent;
    [SerializeField] private RectTransform _resourcesParent;
    [SerializeField] private ProductViewer _productButtonPrefab;

    private List<ICardConfig> _availableToSellItems = new();
    private List<ProductViewer> _productButtons = new();

    private Inventory _inventory;
    private CardData _cardData;
    private PlayerCardConfigContainer _cardHolder;

    public event Action WeaponSold;

    public void Init(Inventory inventory, CardData cardData, PlayerCardConfigContainer cardHolder)
    {
        _inventory = inventory;
        _cardData = cardData;
        _cardHolder = cardHolder;

        gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        foreach (var button in _productButtons)
        {
            button.BuyRequested -= OnSellRequested;
        }
    }

    public void OnActivate()
    {
        gameObject.SetActive(true);
        RenderSellItems();
    }

    private void RenderSellItems()
    {
        ClearOldButtons();
        LoadSellebleCards();

        foreach (var config in _availableToSellItems)
        {
            RectTransform parent = null;

            if (config is WeaponConfig)
                parent = _weaponParent;
            else if (config is AbilityConfig)
                parent = _abilityParent;
            else if (config is BuffConfig)
            {
                Debug.Log(config.Name);
                parent = _buffParent;

            }
            else
                parent = _resourcesParent;

            ProductViewer button = Instantiate(_productButtonPrefab, parent);
            button.Render(config, true);
            button.BuyRequested += OnSellRequested;
            _productButtons.Add(button);
        }
    }

    private void ClearOldButtons()
    {
        foreach (ProductViewer button in _productButtons)
        {
            button.BuyRequested -= OnSellRequested;
            Destroy(button.gameObject);
        }

        _productButtons.Clear();
    }

    private void OnSellRequested(ProductViewer button, IShopConfig config)
    {
        List<CostInfo> sellPrice = config.GetSellCost();
        _inventory.AddResousres(sellPrice);

        if (config is ICardConfig card)
        {
            _availableToSellItems.Remove(card);
            _cardHolder.Remove(card);

            UpdateCardSave(card);
        }

        RenderSellItems();
    }

    private void UpdateCardSave(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            YG2.saves.AllCards = new();

        YG2.saves.AllCards.RemoveAll(savedCard => savedCard.Name == card.Name);
        YG2.saves.AllCards.Add(new CardSaveData(0, card.Name, false ,false ));
        YG2.SaveProgress();
    }

    private void LoadSellebleCards()
    {
        if (YG2.saves.AllCards == null)
            return;

        foreach (var card in _cardData.GetConfigs())
        {
            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.Name == card.Name);
            card.InitFromData(cardData);

            if (card.HasPlayer)
            {
                _availableToSellItems.Add(card);
            }
        }
    }
}

