using System.Collections.Generic;
using UnityEngine;

public class AvanpostContainer : MonoBehaviour
{
    [field: SerializeField] private List<Outpost> _outposts = new List<Outpost>();
    public IReadOnlyList<Outpost> Outposts => _outposts;
}
