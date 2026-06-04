using System.Collections.Generic;
using TowerGuardian.Enums;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]

    public class LevelConfig : ScriptableObject
    {
        [Header("Main Level Settings")]
        [SerializeField] private LevelID _level = LevelID.Level1;
        [SerializeField] private List<QuestType> _quests;
        [SerializeField] private Vector3 _playerSpawnPoint;

        [Header("Day Phase Settings")]
        [SerializeField] private float _dayDuration = 15f;
        [SerializeField] private float _nightDuration = 15f;
        [SerializeField] private float _dayLightIntensity = 1f;
        [SerializeField] private float _nightLightIntensity = 0.2f;
        [SerializeField] private float _transitionDuration = 2f;
        [SerializeField] private Color _dayLightColor = Color.white;
        [SerializeField] private Color _nightLightColor = Color.black;

        [field: SerializeField] public List<Wave> Waves { get; private set; }

        [Header("Level Score Settings")]
        [SerializeField] private float _scorePerTimeOneStar;
        [SerializeField] private float _scorePerTimeTwoStar;
        [SerializeField] private float _scorePerTimeThreeStar;

        public LevelID Level => _level;
        public Vector3 PlayerSpawnPoint => _playerSpawnPoint;

        public float DayDuration => _dayDuration;
        public float NightDuration => _nightDuration;
        public float DayLightIntensity => _dayLightIntensity;
        public float NightLightIntensity => _nightLightIntensity;
        public float TransitionDuration => _transitionDuration;

        public float OneStarScore => _scorePerTimeOneStar;
        public float TwoStarScore => _scorePerTimeTwoStar;
        public float ThreeStarScore => _scorePerTimeThreeStar;

        public Color DayLightColor => _dayLightColor;
        public Color NightLightColor => _nightLightColor;

        public IReadOnlyList<QuestType> Quests => _quests;
    }
}