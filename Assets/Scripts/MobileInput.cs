using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _joystickBackground;
    [SerializeField] private RectTransform _joystickHandle;
    private Vector2 _inputVector = Vector2.zero;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _joystickBackground, eventData.position, eventData.pressEventCamera, out position);

        position.x = (position.x / _joystickBackground.sizeDelta.x) * 2;
        position.y = (position.y / _joystickBackground.sizeDelta.y) * 2;

        _inputVector = new Vector2(position.x, position.y);
        if (_inputVector.magnitude > 1)
            _inputVector = _inputVector.normalized;

        _joystickHandle.anchoredPosition = new Vector2(
            _inputVector.x * (_joystickBackground.sizeDelta.x / 2),
            _inputVector.y * (_joystickBackground.sizeDelta.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public Vector2 GetInput()
    {
        return _inputVector;
    }
}