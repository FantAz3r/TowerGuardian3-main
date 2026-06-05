using System;
using TowerGuardian.Scripts.GamePlayElements.Interaction;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.Buildings
{
    [RequireComponent(typeof(Collider))]
    public class Outpost : InteractionMethod
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public event Action Complited;

        private void Start()
        {
            Disable();
        }

        public override void Interact()
        {
            Complited?.Invoke();
            Disable();
            _particleSystem.Play();
        }
    }
}
