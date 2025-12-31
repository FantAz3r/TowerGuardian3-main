using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))] 

public class ToggleEffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger("Normal");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _animator.SetTrigger("Pressed");
    }
}
