using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class DamageScreen : WindowBase
{
    [Tooltip("Время жизни эффекта в секундах")]
    [SerializeField] private float _lifetime = 1f;

    private Image image;

    public override void Open()
    {
        image = GetComponent<Image>();
        Color color = image.color;
        color.a = 0f; 
        image.color = color;

        float fadeInTime = _lifetime * 0.1f;
        float fadeOutTime = _lifetime * 0.9f;
        base.Open();

        Sequence seq = DOTween.Sequence();

        seq.Append(image.DOFade(1f, fadeInTime));

        seq.Append(image.DOFade(0f, fadeOutTime));

        seq.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }


}
