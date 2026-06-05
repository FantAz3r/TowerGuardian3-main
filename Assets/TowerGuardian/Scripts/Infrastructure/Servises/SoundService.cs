using System.Collections.Generic;
using System.Linq;
using TowerGuardian.Scripts.GamePlayElements.Sounds;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;

namespace TowerGuardian.Scripts.Infrastructure.Servises
{
    public class SoundService : ISoundService
    {
        private HashSet<SoundObject> _soundObjects = new();

        public void Add(SoundObject soundObject)
        {
            _soundObjects.Add(soundObject);
        }

        public void Remove(SoundObject soundObject)
        {
            _soundObjects.Remove(soundObject);
        }

        public void StopAll()
        {
            foreach (var item in _soundObjects.ToList())
            {
                item.StopSound();
            }
        }

        public void ContinueAll()
        {
            foreach (var item in _soundObjects.ToList())
            {
                item.ContinueSound();
            }
        }
    }
}