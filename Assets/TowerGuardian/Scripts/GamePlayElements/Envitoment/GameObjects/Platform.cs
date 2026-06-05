using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Interaction;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class Platform : InteractionMethod
    {
        [field: SerializeField] public WindowType WindowType { get; private set; }

        public override void Interact()
        {
            ServiceLocator.Get<IWindowService>().Open(WindowType);
        }
    }
}