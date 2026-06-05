using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Effects
{
    public class Effect : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
