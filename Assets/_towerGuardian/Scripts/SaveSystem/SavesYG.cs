using System.Collections.Generic;
using TowerGuardian.Enums;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int IdSave;
        public List<CardSaveData> AllCards;
        public CardSaveData CurrentWeapon;
        public List<string> PlayerWeapons;

        public int Coins;
        public int Wood;
        public int Stones;

        public int Level;
        public int UpgradePoints;
        public float CurrentEXP;

        public Vector3 PlayerPosition;
        public LevelID CurrentLevel;
        public LevelID PreviousLevel;

        public List<QuestSaveData> QuestProgress;
        public List<LevelSaveData> LevelsProgress;

        public int CurrentFloor;

        public string Language;
        public bool ShowDamageNumber;
        public bool Mute;
        public List<SoundSaveData> Volumes;

        public bool IsFirstGameSession;
    }
}
