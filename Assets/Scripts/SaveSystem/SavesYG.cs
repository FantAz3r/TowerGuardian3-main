using System.Collections.Generic;

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

        public string CurrentLevel;

        public List<PlayerSaveData> PlayerPositions;

        public List<LevelSaveData> LevelsProgress;
    }
}
