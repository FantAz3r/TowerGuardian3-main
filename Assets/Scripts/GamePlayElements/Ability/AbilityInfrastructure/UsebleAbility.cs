public abstract class UsebleAbility : Ability
{
    public bool IsLock { get; private set; } = false;
    public override AbilityType AbilityType => AbilityType.None;

    public override void Upgrade() { }

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
