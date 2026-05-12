using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float shakeDuration = 1.0f; 
    public float shakeStrength = 0.5f; 
    public int vibrato = 10; 
    public float randomness = 90f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        transform.localPosition = initialPosition;

        transform.DOShakePosition(shakeDuration, shakeStrength, vibrato, randomness)
            .OnComplete(OnShakeComplete); 
    }

    void OnShakeComplete()
    {
       
        transform.localPosition = initialPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Нажмите Пробел для запуска тряски
        {
            TriggerShake();
        }
    }
}
