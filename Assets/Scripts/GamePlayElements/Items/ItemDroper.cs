using UnityEngine;

public class ItemDroper : MonoBehaviour
{
    [SerializeField] private ResourcePiece _resoursePrefab;

    private ObjectPool<ResourcePiece> _pool;
    private float ejectForceMin = 5f;
    private float ejectForceMax = 10f;
    private float ejectRadius = 2f;

    public void SpawnItem(float incomingDamage)
    {
        for (int i = 0; i <= incomingDamage; i++)
        {
            ResourcePiece piece = _pool.Get(); 
            piece.transform.position = CreateSpawnPoint();
            IDemageable health = piece.GetComponent<IDemageable>();
            health.Died += OnPieceDisappear;
            Rigidbody rigidbody = piece.GetComponent<Rigidbody>();

            Vector3 ejectDirection = Random.onUnitSphere;
            ejectDirection.y = Mathf.Abs(ejectDirection.y);
            ejectDirection = ejectDirection.normalized;

            float force = Random.Range(ejectForceMin, ejectForceMax);
            rigidbody.AddForce(ejectDirection * force, ForceMode.Impulse);
        }
    }

    private void OnPieceDisappear(Health health)
    {
        ResourcePiece piece = health.GetComponent<ResourcePiece>();
        health.Died -= OnPieceDisappear;
        _pool.Release(piece);
    }

    private Vector3 CreateSpawnPoint()
    {
        Vector3 spawnPos = transform.position + Random.insideUnitSphere * ejectRadius;
        spawnPos.y = transform.position.y;
        return spawnPos;
    }
}