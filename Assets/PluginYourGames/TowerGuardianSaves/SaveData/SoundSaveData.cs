using System;

namespace TowerGuardian.Scripts.StaticData.Structs.SaveData
{
    [Serializable]
    public struct SoundSaveData
    {
        public string Name;
        public float Volume;

        public SoundSaveData(string name, float volume)
        {
            Name = name;
            Volume = volume;
        }
    }
}
