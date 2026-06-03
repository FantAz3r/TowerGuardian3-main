using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ScaleEffect : MonoBehaviour
{
    private ParticleSystem _particles;
    private Vector3 initialScale;

    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
        float lifetime = _particles.main.duration;

        initialScale = transform.localScale;
        transform.localScale = initialScale * 0.5f;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(initialScale, lifetime * 0.5f).SetEase(Ease.OutQuad));
        seq.Append(transform.DOScale(Vector3.zero, lifetime * 0.5f).SetEase(Ease.InQuad));

        seq.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}