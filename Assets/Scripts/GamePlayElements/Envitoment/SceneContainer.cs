using System.Collections.Generic;
using UnityEngine;

public class SceneContainer : MonoBehaviour, ISceneContainer
{
    [field: SerializeField] public List<Portal> Portals { get; private set; }
   
}
