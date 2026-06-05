using System;
using TowerGuardian.Scripts.Enums;
using UnityEngine;
using UnityEngine.Audio;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct SoundInfo
    {
        public AudioClip AudioClip;
        public AudioMixerGroup AudioGroup;
        public SoundType Type;
    }
}
