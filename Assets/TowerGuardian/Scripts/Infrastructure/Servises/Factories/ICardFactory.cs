using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Factories
{
    public interface ICardFactory
    {
        CardType Type { get; }

        void Create(ICardConfig config);
    }
}