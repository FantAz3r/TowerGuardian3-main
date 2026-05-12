using UnityEngine;

public class LavaRock : Projectile
{
    private float _damage;
    private bool _hasDamagedPlayer = false;

    public void Init(float damage)
    {
        _damage = damage;
    }

    public override void Appear()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerHealth health) && _hasDamagedPlayer ==false)
        {
            Debug.Log(_damage);
            health.TakeDamage(_damage);
            _hasDamagedPlayer = true;
            gameObject.SetActive(false);
        }
    }
}
