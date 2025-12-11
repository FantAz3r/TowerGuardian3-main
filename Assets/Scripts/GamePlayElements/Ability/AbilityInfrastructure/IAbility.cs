public interface IAbility 
{
    AbilityType AbilityType { get; }
    void Enable();
    void Upgrade();
    void Remove();
}
