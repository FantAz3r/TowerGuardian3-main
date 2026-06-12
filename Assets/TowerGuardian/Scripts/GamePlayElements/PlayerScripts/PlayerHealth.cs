using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;

namespace TowerGuardian.Scripts.GamePlayElements.PlayerScripts
{
    public class PlayerHealth : Health
    {
        private IWindowService _windowService;

        protected override void Awake()
        {
            base.Awake();
            _windowService = ServiceLocator.Get<IWindowService>();
        }

        public override void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                return;
            }

            base.TakeDamage(damage);
            _windowService.Open(WindowType.DamageScreen);
        }

        public void HealMaxHealth()
        {
            Heal(MaxHealth);
        }
    }
}