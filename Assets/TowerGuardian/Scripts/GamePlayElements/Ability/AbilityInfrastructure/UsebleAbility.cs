namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure
{
    public abstract class UsebleAbility : Ability
    {
        public bool IsLock { get; private set; }

        public virtual void LockAbility()
        {
            IsLock = true;
        }

        public virtual void UnlockAbility()
        {
            IsLock = false;
        }

        public abstract void Use();
    }
}
