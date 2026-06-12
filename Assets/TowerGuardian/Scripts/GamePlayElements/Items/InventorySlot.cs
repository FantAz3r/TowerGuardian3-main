using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.UI.Elements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.GamePlayElements.Items
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Image _weaponImage;
        [SerializeField]
        private Image _abilityImage;
        [SerializeField]
        private CardType _slotType;

        private PlayerCardConfigContainer _cardHolrer;

        [field: SerializeField]
        public bool IsActiveSlot { get; private set; }

        public Image CurrentImage { get; private set; }

        public UIItem CurrentItem { get; private set; }

        private void Awake()
        {
            _cardHolrer = ServiceLocator.Get<IGameFactory>().Player.CardHolder;
        }

        public void Init(CardType slotType, bool isActiveSlot)
        {
            _slotType = slotType;
            IsActiveSlot = isActiveSlot;

            if (_slotType == CardType.Weapon)
            {
                CurrentImage = _weaponImage;
            }
            else if (_slotType == CardType.Ability)
            {
                CurrentImage = _abilityImage;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (CurrentItem != null)
            {
                return;
            }

            if (_slotType == CardType.None)
            {
                return;
            }

            var itemTransform = eventData.pointerDrag.transform;
            var draggedItem = itemTransform.GetComponent<UIItem>();

            if (_slotType != CardType.Any && draggedItem.SlotConfig.GetCardType() != _slotType)
            {
                return;
            }

            itemTransform.SetParent(transform);
            itemTransform.localPosition = Vector3.zero;

            if (CurrentImage != null)
            {
                CurrentImage.enabled = false;
            }
        }

        public void SetItem(UIItem item)
        {
            CurrentItem = item;
        }

        public void AddItem(UIItem item)
        {
            SetItem(item);

            if (IsActiveSlot)
            {
                _cardHolrer.AddCard(item.SlotConfig);
            }
        }

        public void RemoveItem(UIItem item)
        {
            if (IsActiveSlot)
            {
                _cardHolrer.SaveInInventory(item.SlotConfig);
            }

            SetItem(null);
        }
    }
}
