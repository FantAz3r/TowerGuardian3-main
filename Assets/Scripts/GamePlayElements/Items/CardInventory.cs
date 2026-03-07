using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInventory : PauseWindow
{
    [SerializeField] private InventorySlot _itemSlotPrefab;
    [SerializeField] private UIItem _itemPrefab;
    [SerializeField] private GridLayoutGroup _parent;
    [SerializeField] private InventoryStats _inventoryStats;
    [SerializeField] private TMP_Text _tipText;

    private Canvas _canvas;
    private CardData _cardData;
    private List<InventorySlot> _slots = new();

    protected override void Awake()
    {
        base.Awake();
        _cardData = Resources.Load<CardData>(GameConstants.CardData);
        _canvas = GetComponentInParent<Canvas>();
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
                if (card.IsBought && card.Level > 0)
                {
                    InventorySlot slot = Instantiate(_itemSlotPrefab, _parent.transform);
                    _slots.Add(slot);
                    slot.Init(CardType.Any, false);

                    if (card.HasPlayer == false)
                    {
                        UIItem item = Instantiate(_itemPrefab, slot.transform);
                        item.Init(transform, _canvas);
                        item.StatsButton.Init(_inventoryStats);
                        item.SetConfig(card);
                        slot.AddItem(item);
                    }

                    slot.transform.SetAsFirstSibling();
                }
            }
        }

        if(_slots.Count == 0)
        {
            _tipText.gameObject.SetActive(true);
        }
        else
        {
            _tipText.gameObject.SetActive(false);
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
