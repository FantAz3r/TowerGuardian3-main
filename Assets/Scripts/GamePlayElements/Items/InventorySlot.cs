using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private CardType _slotType;
    [SerializeField] private bool _isActiveSlot;
    private PlayerCardConfigContainer _cardHolrer;

    public UIItem CurrentItem { get; private set; }

    private void Awake()
    {
        _cardHolrer = ServiceLocator.Get<IGameFactory>().Player.CardHolder;
    }

    public void Init(CardType slotType, bool isActiveSlot)
    {
        _slotType = slotType;
        _isActiveSlot = isActiveSlot;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (CurrentItem != null)
            return;

        if (_slotType == CardType.None)
            return;

        var itemTransform = eventData.pointerDrag.transform;
        var draggedItem = itemTransform.GetComponent<UIItem>();

        if (_slotType != CardType.Any && draggedItem.SlotConfig.GetCardType() != _slotType)
            return;

        itemTransform.SetParent(transform);
        itemTransform.localPosition = Vector3.zero;

        AddItem(draggedItem);
    }

    public void SetItem(UIItem item)
    {
        CurrentItem = item;
    }

    public void AddItem(UIItem item)
    {
        SetItem(item);

        if (_isActiveSlot)
        {
            _cardHolrer.AddCard(item.SlotConfig);
        }
    }

    public void RemoveItem(UIItem item)
    {
        if (_isActiveSlot)
        {
            _cardHolrer.SaveInInventory(item.SlotConfig);
        }

        SetItem(null);
    }
}
