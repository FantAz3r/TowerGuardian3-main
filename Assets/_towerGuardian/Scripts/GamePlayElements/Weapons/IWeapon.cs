using TowerGuardian.StaticData;

public interface IWeapon
{
    WeaponConfig Config { get; }
    void Attack();
    void TakeOff();
}