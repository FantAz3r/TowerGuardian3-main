using System.Collections.Generic;

public interface ISceneContainer 
{
    List<Portal> Portals { get; }
    List<SpawnerActivator> SpawnPoints { get; }
}
