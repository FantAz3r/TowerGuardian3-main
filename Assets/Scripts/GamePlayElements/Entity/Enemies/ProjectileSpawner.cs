using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : BaseSpawner
{
    private Dictionary<ProjectileType, ObjectPool<Projectile>> _pools = new ();
    private ProjectileData _projectileData;

    public ProjectileSpawner()
    {
        _projectileData = Resources.Load<ProjectileData>(GameConstants.ProjectileData);

        foreach (var projectile in _projectileData.ProjectileInfos)
        {
            _pools.Add(projectile.Type, new ObjectPool<Projectile>(projectile.Prefab, 0, true));
        }
    }

    public override Projectile Spawn(ProjectileType type, Vector3 position, Transform parent = null)
    {
        Projectile projectile = Object.Instantiate(_projectileData.GetProJectile(type), position, Quaternion.identity);
        projectile.Appear();
        return projectile;
    }

    public override void DestroyPool()
    {
        foreach (var pair in _pools)
        {
            pair.Value.DestroyPool();
        }
    }

    public override SpawnerType GetSpawnerType() => SpawnerType.Projectile;
}