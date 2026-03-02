using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class JoystickToggle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private OnScreenStick _joystick; 

    public void Init(OnScreenStick joystick)
    {
        _joystick = joystick;
        _joystick.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Open");
        _joystick.gameObject.SetActive(true);


        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _joystick.transform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        _joystick.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Close");

        //_joystick.gameObject.SetActive(false);
    }
}

