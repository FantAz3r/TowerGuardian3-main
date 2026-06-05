using TowerGuardian.Scripts.StaticData.Configs;

namespace TowerGuardian.Scripts.GamePlayElements.Weapons
{
    public interface IWeapon
    {
        WeaponConfig Config { get; }
        void Attack();
        void TakeOff();
    }
}