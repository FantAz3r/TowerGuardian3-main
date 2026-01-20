using System;

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
