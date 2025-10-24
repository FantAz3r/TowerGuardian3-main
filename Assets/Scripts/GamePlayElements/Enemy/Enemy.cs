using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += Disabled;
    }

    private void OnDisable()
    {
        _health.Died -= Disabled;
    }

    private void Disabled(IDemageable demageable)
    {
        gameObject.SetActive(false);
    }
}
