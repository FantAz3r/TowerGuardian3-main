using TowerGuardian.Scripts.GamePlayElements.Sounds;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface ISoundService : IService
    {
        void Remove(SoundObject soundObject);
        void Add(SoundObject soundObject);
        void StopAll();
        void ContinueAll();
    }
}