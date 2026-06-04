using System.Linq;
using TowerGuardian.Enums;
using TowerGuardian.Factories;
using TowerGuardian.Infrastructure;
using TowerGuardian.StaticData;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private InventorySlot _slotPrefab;
    [SerializeField] private UIItem _itemPrefab;
    [SerializeField] private VerticalLayoutGroup _abilitySlotsParent;
    [SerializeField] private VerticalLayoutGroup _weaponSlotsParent;
    [SerializeField] private InventoryStats _inventoryStats;
    
    private Canvas _mainCanvas;
    private PlayerCardConfigContainer _cardHolder;
    private CardData _cardData;

    private void Awake()
    {
        _cardHolder = ServiceLocator.Get<IGameFactory>().Player.CardHolder;
        _cardData = Resources.Load<CardData>(GameConstants.CardData);
        _mainCanvas = GetComponentInParent<Canvas>();

        CreateSlotsWithItems(CardType.Ability, _cardHolder.MaxAbilityCards, _abilitySlotsParent.transform);
        CreateSlotsWithItems(CardType.Weapon, _cardHolder.MaxWeaponCards, _weaponSlotsParent.transform);
    }

    private void CreateSlotsWithItems(CardType slotType, int maxSlots, Transform parent)
    {
        var items = _cardData.GetConfigs()
                    .Where(card => card.HasPlayer && card.GetCardType() == slotType)
                    .ToList();

        for (int i = 0; i < maxSlots; i++)
        {
            InventorySlot slot = Instantiate(_slotPrefab, parent.transform);
            slot.Init(slotType, true);

            if (i < items.Count)
            {
                var card = items[i];
                UIItem item = Instantiate(_itemPrefab, slot.transform);
                item.Init(transform, _mainCanvas);
                item.SetConfig(card);
                item.StatsButton.Init(_inventoryStats);
                slot.SetItem(item);
                continue;
            }

            slot.CurrentImage.enabled = true;
        }
    }
}
