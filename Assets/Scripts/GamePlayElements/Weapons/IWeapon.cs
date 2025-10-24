public interface IWeapon
{
    WeaponConfig Config { get; }
    void Init(AttackZone attackZone);
    void Attack();
    void TakeOff();
}