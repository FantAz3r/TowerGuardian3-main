using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInventory : PauseWindow
{
    [SerializeField] private InventorySlot _itemSlotPrefab;
    [SerializeField] private UIItem _itemPrefab;
    [SerializeField] private GridLayoutGroup _parent;

    private Canvas _canvas;
    private CardData _cardData;
    private List<InventorySlot> _slots = new();
    private PlayerCardConfigContainer _cardHolder;

    protected override void Awake()
    {
        base.Awake();
        _cardData = Resources.Load<CardData>(GameConstants.CardData);
        _canvas = GetComponentInParent<Canvas>();
        _cardHolder = ServiceLocator.Get<IGameFactory>().Player.CardHolder;
    }

    private void OnEnable()
    {
        ViewAll();
    }

    public void ViewAll()
    {
        ViewSlots(card => card is WeaponConfig || card is AbilityConfig);
    }

    public void ViewWeapons()
    {

        ViewSlots(card => card is WeaponConfig);
    }

    public void ViewAbilities()
    {
        ViewSlots(card => card is AbilityConfig);
    }

    private void ViewSlots(Func<ICardConfig, bool> filter)
    {
        RemoveAllSlots();

        foreach (var card in _cardData.GetConfigs())
        {
            if (filter(card))
            {
                if (card.IsBought)
                {
                    InventorySlot slot = Instantiate(_itemSlotPrefab, _parent.transform);
                    slot.Init(CardType.Any, false);

                    if (card.HasPlayer == false)
                    {
                        UIItem item = Instantiate(_itemPrefab, slot.transform);
                        item.Init(transform, _canvas);
                        item.SetConfig(card);
                        slot.AddItem(item);
                    }

                    slot.transform.SetAsFirstSibling();
                }
            }
        }
    }

    private void RemoveAllSlots()
    {
        var children = _parent.GetComponentsInChildren<InventorySlot>();

        foreach (var child in children)
        {
            Destroy(child.gameObject);
        }

        _slots.Clear();
    }
}
