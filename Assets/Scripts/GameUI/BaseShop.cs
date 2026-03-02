using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public abstract class BaseShop : PauseWindow
{
    [SerializeField] private ResourceViewer _resourceView;
    [SerializeField] private RectTransform _weaponContentParent, _abilitiesContentParent, _buffContentParent;
    [SerializeField] private Button _weaponButton, _abilityButton, _buffButton;
    [SerializeField] private ProductViewer _productButtonPrefab;

    private List<ICardConfig> _configs = new();
    private List<ProductViewer> _productButtons = new();
    private int _weaponCardCount = 0, _abilityCardCount = 0, _buffCardCount = 0;
    private RectTransform _currentContentPanel;

    public Player Player { get; private set; }
    public List<ProductViewer> ProductButtons => _productButtons;
    public RectTransform WeaponContentParent => _weaponContentParent;
    public CardData CardData { get; private set; }
    public List<ICardConfig> Configs => _configs;


    protected override void Awake()
    {
        base.Awake();
        CardData = Resources.Load<CardData>(GameConstants.CardData);
        Player = ServiceLocator.Get<IGameFactory>().Player;
    }

    private void OnDestroy()
    {
        foreach (var button in _productButtons)
        {
            button.BuyRequested -= OnTradeRequested;
        }
    }

    public override void Open()
    {
        base.Open();
        _weaponContentParent.gameObject.SetActive(false);
        _abilitiesContentParent.gameObject.SetActive(false);
        _buffContentParent.gameObject.SetActive(false);

        ClearOldButtons();
        LoadCards();
        SetParents();
    }

    protected void ClearOldButtons()
    {
        foreach (ProductViewer button in _productButtons)
        {
            button.BuyRequested -= OnTradeRequested;
            Destroy(button.gameObject);
        }

        _configs.Clear();
        _productButtons.Clear();
    }

    protected ProductViewer CreateButton(RectTransform parent)
    {
        var button = Instantiate(_productButtonPrefab, parent);
        button.BuyRequested += OnTradeRequested;
        _productButtons.Add(button);
        return button;
    }

    protected virtual void LoadCards()
    {
        if (YG2.saves.AllCards == null)
        {
            OnNoSaveData();
            return;
        }

        foreach (var card in CardData.GetConfigs())
        {
            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);
            card.InitFromData(cardData);
        }
    }

    protected virtual CardSaveData CreateSaveData(ICardConfig card)
    {
        return new CardSaveData(0, card.ID, false, false);
    }

    protected void UpdateCardSave(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            YG2.saves.AllCards = new List<CardSaveData>();

        YG2.saves.AllCards.RemoveAll(savedCard => savedCard.ID == card.ID);
        YG2.saves.AllCards.Add(CreateSaveData(card));
        YG2.SaveProgress();
    }

    protected virtual void OnNoSaveData()
    {
    }

    protected void SetParents()
    {
        _weaponButton.gameObject.SetActive(true);
        _buffButton.gameObject.SetActive(true);
        _abilityButton.gameObject.SetActive(true);

        foreach (var config in Configs)
        {
            RectTransform parent = null;

            if (config is WeaponConfig)
            {
                parent = _weaponContentParent;

                _weaponCardCount++;

            }
            else if (config is AbilityConfig)
            {
                parent = _abilitiesContentParent;

                _abilityCardCount++;
            }
            else if (config is BuffConfig)
            {
                parent = _buffContentParent;

                _buffCardCount++;
            }
            else
                return;

            OnParentFounded(parent, config);
        }

        if (_weaponCardCount == 0)
            _weaponButton.gameObject.SetActive(false);

        if (_buffCardCount == 0)
            _buffButton.gameObject.SetActive(false);

        if (_abilityCardCount == 0)
            _abilityButton.gameObject.SetActive(false);
    }

    protected abstract void OnTradeRequested(ProductViewer button, ICardConfig config);

    protected abstract void OnParentFounded(RectTransform Parent, ICardConfig config);
}
