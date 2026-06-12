using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerGuardian.Scripts.UI.Elements
{
    [RequireComponent(typeof(Animator))]

    public class ToggleEffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private readonly int _normal = Animator.StringToHash("Normal");
        private readonly int _highlighted = Animator.StringToHash("Highlighted");
        private readonly int _pressed = Animator.StringToHash("Pressed");
        [SerializeField]
        private AudioClip _pressedSound;
        [SerializeField]
        private AudioClip _highlightedSound;

        private Animator _animator;
        private ISpawnerService _spawnerService;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spawnerService = ServiceLocator.Get<ISpawnerService>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _animator.SetTrigger(_highlighted);
            _spawnerService.SendSoundReqest(_highlightedSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _animator.SetTrigger(_normal);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _animator.SetTrigger(_pressed);
            _spawnerService.SendSoundReqest(_pressedSound);
        }
    }
}