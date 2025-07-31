using UnityEngine;

public class EnemyFactory
{
    public Enemy Create(Vector3 position, Enemy enemyPrefab)
    {
        Enemy enemyInstance = Object.Instantiate(enemyPrefab, position, Quaternion.identity);

        return enemyInstance;
    }
}
