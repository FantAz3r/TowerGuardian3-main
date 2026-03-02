using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [field: SerializeField] public Image SlotImag { get; private set; }
    [field: SerializeField] public TMP_Text ItemLevelText { get; private set; }

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    private Transform _inventory;
    private Canvas _mainCanvas;
    private Transform _previousParent;
    private InventorySlot _previousSlot;
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
        _previousParent = transform.parent;
        _previousSlot = _previousParent.GetComponent<InventorySlot>();

        transform.SetParent(_inventory);
        transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventorySlot currentSlot = transform.parent.GetComponent<InventorySlot>();

        if (currentSlot == null || currentSlot.CurrentItem != this)
        {
            transform.SetParent(_previousParent);
            transform.localPosition = Vector3.zero;

        }
        else
        {
            if (_previousSlot != null && _previousSlot != currentSlot)
            {

                _previousSlot.RemoveItem(this);

            }

            transform.localPosition = Vector3.zero;
        }

        _canvasGroup.blocksRaycasts = true;
    }
}
