using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.Buildings;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment
{
    public class AvanpostContainer : MonoBehaviour
    {
        [field: SerializeField]
        private List<Outpost> _outposts = new List<Outpost>();

        public IReadOnlyList<Outpost> Outposts => _outposts;
    }
}
