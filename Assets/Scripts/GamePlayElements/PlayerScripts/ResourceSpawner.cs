using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _resourcePrefabs;
    [SerializeField] private int _itemAmount = 50;

    [SerializeField] private float _spawnZoneWidth = 20f;
    [SerializeField] private float _spawnZoneLength = 20f;
    [SerializeField] private float _minDistanceBetweenResources = 1.5f;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _resourceLayer;


    private void Start()
    {
        SpawnObject(_itemAmount);
    }

    private void SpawnObject(int itemAmount)
    {
        int spawned = 0;
        int maxAttempts = 1500;
        int attempts = 0;

        while (spawned < itemAmount && attempts < maxAttempts)
        {
            Vector3 spawnPos = GetRandomPosition();

            if (IsValidSpawnPosition(spawnPos))
            {
                GameObject resourcePrefab = _resourcePrefabs[Random.Range(0, _resourcePrefabs.Length)];
                GameObject spawnedResource = Instantiate(resourcePrefab, spawnPos, Quaternion.identity);
                spawnedResource.transform.SetParent(transform);

                spawned++;
            }

            attempts++;
        }
    }

    private Vector3 GetRandomPosition()
    {
        float decreaseValue = 2f;
        float offsetY = 0f;
        int randomX = Random.Range(
            Mathf.CeilToInt(-_spawnZoneLength / decreaseValue),
            Mathf.FloorToInt(_spawnZoneLength / decreaseValue) + 1
        );

        int randomZ = Random.Range(
            Mathf.CeilToInt(-_spawnZoneWidth / decreaseValue),
            Mathf.FloorToInt(_spawnZoneWidth / decreaseValue) + 1
        );

        Vector3 position = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );

        RaycastHit hit;

        if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity, _groundLayer))
        {
            position.y = hit.point.y + offsetY;
        }
        else
        {
            position.y = transform.position.y + offsetY;
        }

        return position;
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _minDistanceBetweenResources, _resourceLayer);
        return colliders.Length == 0;
    }
}