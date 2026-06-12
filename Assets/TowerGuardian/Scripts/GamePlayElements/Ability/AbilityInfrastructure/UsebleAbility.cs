namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure
{
    public abstract class UsebleAbility : Ability
    {
        public bool IsLock { get; private set; }

        public abstract void Use();

        protected void LockAbility()
        {
            IsLock = true;
        }

        protected void UnlockAbility()
        {
            IsLock = false;
        }
    }
}
