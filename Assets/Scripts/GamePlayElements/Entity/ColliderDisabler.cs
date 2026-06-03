using UnityEngine;

public class ColliderDisabler : MonoBehaviour
{
    private Collider _collider;
    private Health _health;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        _health.Died += Disable;
    }

    private void OnDisable()
    {
        _health.Died -= Disable;
    }

    public void Disable()
    {
        _collider.enabled = false;
    }

    public void Enable()
    {
        _collider.enabled = true;
    }
}