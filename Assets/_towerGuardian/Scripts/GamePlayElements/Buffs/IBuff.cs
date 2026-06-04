using TowerGuardian.Enums;
using TowerGuardian.StaticData;

public interface IBuff : IItem<BuffType, BuffConfig>
{
    void SetConfig(BuffConfig config);
}
