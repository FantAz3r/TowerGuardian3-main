public interface IWeapon
{
    WeaponType WeaponType { get; }
    void Init(AttackZone attackZone);
    void Attack();
    void TakeOff();
    void ApplyDamage();
}