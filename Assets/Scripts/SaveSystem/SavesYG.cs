using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int idSave;
        public List<CardSaveData> AllCards;
        public CardSaveData CurrentWeapon;
        public List<string> PlayerWeapons;

        public int Coins;
        public int Wood;
        public int Stones;

        public int Level;
        public int UpgradePoints;
        public float CurrentEXP;

        public LevelID CurrentLevel;
        public Vector3 PlayerPosition;

        public List<QuestSaveData> QuestProgress;
        public List<LevelSaveData> LevelsProgress;

        public int CurrentFloor;

        public string Language;
        public bool ShowDamageNumber;
        public bool Mute;
        public List<SoundSaveData> Volumes;
    }
}
