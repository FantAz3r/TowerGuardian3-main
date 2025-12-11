public abstract class UsebleAbility : Ability
{
    public override AbilityType AbilityType => AbilityType.None;

    public override void Upgrade()
    {
    }

    public abstract void Use();
}
