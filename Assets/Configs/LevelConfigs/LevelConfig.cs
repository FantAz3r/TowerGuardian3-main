using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Day Phase Settings")]
    [SerializeField] private LevelID _level = LevelID.Level1;
    [SerializeField] private float _dayDuration = 15f;
    [SerializeField] private float _nightDuration = 15f;
    [SerializeField] private float _dayLightIntensity = 1f;
    [SerializeField] private float _nightLightIntensity = 0.2f;
    [SerializeField] private float _transitionDuration = 2f;
    [SerializeField] private Color _dayLightColor = Color.white;
    [SerializeField] private Color _nightLightColor = Color.black;

    [Header("Enemy Spawn Settings")]
    [SerializeField] private float _minSpawnDistance = 10f;
    [SerializeField] private float _maxSpawnDistance = 40f;
    [SerializeField] private float _nightSpawnDelay = 3f;
    [SerializeField] private float _daySpawnDelay = 8f;

    public LevelID Level => _level;
    public float DayDuration => _dayDuration;
    public float NightDuration => _nightDuration;
    public Color DayLightColor => _dayLightColor;
    public Color NightLightColor => _nightLightColor;
    public float DayLightIntensity => _dayLightIntensity;
    public float NightLightIntensity => _nightLightIntensity;
    public float TransitionDuration => _transitionDuration;

    public float MinSpawnDistance => _minSpawnDistance;
    public float MaxSpawnDistance => _maxSpawnDistance;
    public float NightSpawnDelay => _nightSpawnDelay;
    public float DaySpawnDelay => _daySpawnDelay;
}