using UnityEngine;

[RequireComponent(typeof(Health))]
public class ResourceItem : MonoBehaviour
{
    private IDemageable _health;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void Die(IDemageable demageable)
    {
        Destroy(gameObject);
    }
}
