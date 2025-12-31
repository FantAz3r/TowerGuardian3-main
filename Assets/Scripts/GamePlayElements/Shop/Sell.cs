using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Sell : MonoBehaviour
{
    [SerializeField] private RectTransform _weaponParent;
    [SerializeField] private Button _weaponButton;
    [SerializeField] private RectTransform _abilityParent;
    [SerializeField] private Button _abilityButton;
    [SerializeField] private RectTransform _buffParent;
    [SerializeField] private Button _buffButton;

    [SerializeField] private SellResources _resourcesPanel;
    [SerializeField] private ProductViewer _productButtonPrefab;


    private List<ICardConfig> _availableToSellItems = new();
    private List<ProductViewer> _productButtons = new();

    private int _weaponCardCount = 0;
    private int _abilityCardCount = 0;
    private int _buffCardCount = 0;

    private Inventory _inventory;
    private CardData _cardData;
    private PlayerCardConfigContainer _cardHolder;
    private CardSelectionMenu _cardMenu;

    public event Action WeaponSold;

    public void Init(Inventory inventory, CardData cardData, PlayerCardConfigContainer cardHolder, CardSelectionMenu cardMenu)
    {
        _inventory = inventory;
        _cardData = cardData;
        _cardHolder = cardHolder;
        _cardMenu = cardMenu;

        _resourcesPanel.Init(_inventory);
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

        _resourcesPanel.gameObject.SetActive(true);
        _weaponParent.gameObject.SetActive(false);
        _abilityParent.gameObject.SetActive(false);
        _buffParent.gameObject.SetActive(false);
        _resourcesPanel.RenderSellItems();
    }

    private void RenderSellItems()
    {
        _weaponButton.gameObject.SetActive(true);
        _buffButton.gameObject.SetActive(true);
        _abilityButton.gameObject.SetActive(true);

        ClearOldButtons();
        LoadSellebleCards();

        foreach (var config in _availableToSellItems)
        {
            RectTransform parent = null;

            if (config is WeaponConfig)
            {
                parent = _weaponParent;
                Debug.Log(config.Name + "weapon");

                _weaponCardCount++;

            }
            else if (config is AbilityConfig)
            {
                parent = _abilityParent;
                Debug.Log(config.Name + "abil");

                _abilityCardCount++;
            }
            else if (config is BuffConfig)
            {
                parent = _buffParent;
                Debug.Log(config.Name + "buff");

                _buffCardCount++;
            }
            else
                return;

            ProductViewer button = Instantiate(_productButtonPrefab, parent);
            button.Render(config, true);
            button.BuyRequested += OnSellRequested;
            _productButtons.Add(button);
        }

        if (_weaponCardCount == 0)
            _weaponButton.gameObject.SetActive(false);

        if (_buffCardCount == 0)
            _buffButton.gameObject.SetActive(false);

        if (_abilityCardCount == 0)
            _abilityButton.gameObject.SetActive(false);
    }

    private void ClearOldButtons()
    {
        foreach (ProductViewer button in _productButtons)
        {
            button.BuyRequested -= OnSellRequested;
            Destroy(button.gameObject);
        }

        _availableToSellItems.Clear();
        _productButtons.Clear();
    }

    private void OnSellRequested(ProductViewer button, IShopConfig config)
    {
        List<CostInfo> sellPrice = config.GetSellCosts();
        _inventory.AddResousres(sellPrice);

        if (config is ICardConfig card)
        {
            _availableToSellItems.Remove(card);
            _cardHolder.Remove(card);

            UpdateCardSave(card);
            _cardMenu.AddPoints(card.Level);
        }

        RenderSellItems();
    }

    private void UpdateCardSave(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            YG2.saves.AllCards = new();

        YG2.saves.AllCards.RemoveAll(savedCard => savedCard.ID == card.ID);
        YG2.saves.AllCards.Add(new CardSaveData(0, card.ID, false, false));
        YG2.SaveProgress();
    }

    private void LoadSellebleCards()
    {
        if (YG2.saves.AllCards == null)
            return;

        foreach (var card in _cardData.GetConfigs())
        {
            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);
            card.InitFromData(cardData);

            if (card.HasPlayer)
            {
                _availableToSellItems.Add(card);
            }
        }
    }
}

