using TowerGuardian.Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))] 

public class ToggleEffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip _pressedSound;
    [SerializeField] private AudioClip _highlightedSound;

    private Animator _animator;
    private ISpawnerService _spawnerService;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger("Highlighted");
        _spawnerService.SendSoundReqest(_highlightedSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger("Normal");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _animator.SetTrigger("Pressed");
        _spawnerService.SendSoundReqest(_pressedSound);
    }
}
