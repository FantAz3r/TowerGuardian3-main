using TowerGuardian.Enums;
using TowerGuardian.StaticData;

namespace TowerGuardian.Factories
{
    public interface ICardFactory
    {
        CardType Type { get; }
        void Create(ICardConfig config);
    }
}