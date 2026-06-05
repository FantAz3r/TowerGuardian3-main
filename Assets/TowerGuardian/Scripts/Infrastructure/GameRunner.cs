using TowerGuardian.Scripts.Infrastructure.FSM.States;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private EntryPoint _entryPoint;

        private void Awake()
        {
            _entryPoint = FindObjectOfType<EntryPoint>();

            if (_entryPoint == null)
                Instantiate(_entryPoint);
        }
    }
}