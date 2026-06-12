using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Items;
using TowerGuardian.Scripts.StaticData.Configs;

namespace TowerGuardian.Scripts.GamePlayElements.Buffs.BuffInfrastructure
{
    public interface IBuff : IItem<BuffType, BuffConfig>
    {
    }
}
