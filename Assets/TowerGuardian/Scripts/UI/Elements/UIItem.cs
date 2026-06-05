using TMPro;
using TowerGuardian.Scripts.GamePlayElements.Items;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Elements
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;

        private Transform _inventory;
        private Canvas _mainCanvas;
        private Transform _previousParent;
        private InventorySlot _previousSlot;
        [field: SerializeField] public Image SlotImag { get; private set; }
        [field: SerializeField] public TMP_Text ItemLevelText { get; private set; }
        [field: SerializeField] public StatsButton StatsButton { get; private set; }
        public ICardConfig SlotConfig { get; private set; }

        public void Init(Transform inventory, Canvas mainCanvas)
        {
            _inventory = inventory;
            _mainCanvas = mainCanvas;
        }

        public void SetConfig(ICardConfig config)
        {
            SlotConfig = config;
            SlotImag.sprite = SlotConfig.Icon;
            ItemLevelText.text = SlotConfig.Level.ToString();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            StatsButton.OnClick();

            _previousParent = transform.parent;
            _previousSlot = _previousParent.GetComponent<InventorySlot>();

            transform.SetParent(_inventory);
            transform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InventorySlot newSlot = null;

            if (transform.parent != null)
                newSlot = transform.parent.GetComponent<InventorySlot>();

            if (newSlot == null || newSlot.CurrentItem != null)
            {
                transform.SetParent(_previousParent);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                if (_previousSlot != null && _previousSlot != newSlot)
                {
                    _previousSlot.RemoveItem(this);
                    newSlot.AddItem(this);

                    if (_previousSlot.CurrentImage != null)
                    {
                        _previousSlot.CurrentImage.enabled = true;
                    }
                }
                else if (_previousSlot == newSlot)
                {
                    transform.localPosition = Vector3.zero;
                }
            }

            _canvasGroup.blocksRaycasts = true;
        }
    }
}