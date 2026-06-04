using System;
using TowerGuardian.Enums;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public struct SoundInfo 
{
    public AudioClip AudioClip;
    public AudioMixerGroup AudioGroup;
    public SoundType Type;
}
