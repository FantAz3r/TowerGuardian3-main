using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]

public class ButtonEffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Sprite _highlightedSprite;
    [SerializeField] private Sprite _normalSprite;

    private Image _buttonImage;
    private Animator _animator;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _buttonImage.sprite = _normalSprite;
        _animator.SetTrigger("Normal");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonImage.sprite = _highlightedSprite;
        _animator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonImage.sprite = _normalSprite;
        _animator.SetTrigger("Normal");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _animator.SetTrigger("Pressed");
    }
}
