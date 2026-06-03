using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 1.0f; 
    [SerializeField] private float _shakeStrength = 0.5f; 
    [SerializeField] private int _vibrato = 10; 
    [SerializeField] private float _randomness = 90f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        transform.localPosition = initialPosition;

        transform.DOShakePosition(_shakeDuration, _shakeStrength, _vibrato, _randomness)
            .OnComplete(OnShakeComplete); 
    }

    private void OnShakeComplete()
    {
        transform.localPosition = initialPosition;
    }
}
