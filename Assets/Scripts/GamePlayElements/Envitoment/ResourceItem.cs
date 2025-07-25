using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ItemDroper))]
public class ResourceItem : MonoBehaviour
{
    [SerializeField] private int _resourceAmount = 2;

    private ItemDroper _droper;
    private IDemageable _health;

    private void Awake()
    {
        _droper = GetComponent<ItemDroper>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthLost += Extract;
    }

    private void OnDisable()
    {
        _health.HealthLost -= Extract;
    }

    private void Extract(float damage)
    {
        _droper.SpawnItem(transform.position, damage);
    }
}
