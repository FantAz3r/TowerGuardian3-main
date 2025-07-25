using UnityEngine;

public class ItemDroper : MonoBehaviour
{
    [SerializeField] private ResourcePiece _resoursePrefab;

    private float ejectForceMin = 5f;
    private float ejectForceMax = 10f;
    private float ejectRadius = 2f;

    public void SpawnItem(Vector3 position, float incomingDamage)
    {
        for (int i = 0; i <= incomingDamage; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * ejectRadius;
            spawnPos.y = transform.position.y;

            ResourcePiece piece = Instantiate(_resoursePrefab, spawnPos, Quaternion.identity);
            Rigidbody rigidbody = piece.GetComponent<Rigidbody>();

            Vector3 ejectDirection = Random.onUnitSphere;
            ejectDirection.y = Mathf.Abs(ejectDirection.y);
            ejectDirection = ejectDirection.normalized;

            float force = Random.Range(ejectForceMin, ejectForceMax);
            rigidbody.AddForce(ejectDirection * force, ForceMode.Impulse);
        }
    }
}