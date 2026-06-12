using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]

    public class ButtonEffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Sprite _highlightedSprite;
        [SerializeField]
        private Sprite _normalSprite;

        [SerializeField]
        private Color _highlightedColor = Color.white;
        [SerializeField]
        private Color _normalColor = Color.white;

        [SerializeField]
        private AudioClip _pressedSound;
        [SerializeField]
        private AudioClip _highlightedSound;

        private Image _buttonImage;
        private Animator _animator;
        private ISpawnerService _spawnerService;

        private void Awake()
        {
            _buttonImage = GetComponent<Image>();
            _animator = GetComponent<Animator>();
            _spawnerService = ServiceLocator.Get<ISpawnerService>();
        }

        private void OnDisable()
        {
            _buttonImage.color = _normalColor;
            _buttonImage.sprite = _normalSprite;
            _animator.SetTrigger("Normal");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonImage.color = _highlightedColor;
            _buttonImage.sprite = _highlightedSprite;
            _animator.SetTrigger("Highlighted");
            _spawnerService.SendSoundReqest(_highlightedSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonImage.color = _normalColor;
            _buttonImage.sprite = _normalSprite;
            _animator.SetTrigger("Normal");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _animator.SetTrigger("Pressed");
            _spawnerService.SendSoundReqest(_pressedSound);
        }
    }
}