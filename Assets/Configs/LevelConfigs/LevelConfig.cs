using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private int _level = 1;
    [SerializeField] private float _dayDuration = 15f;
    [SerializeField] private float _nightDuration = 15f;
    [SerializeField] private Color _dayLightColor = Color.white;
    [SerializeField] private Color _nightLightColor = Color.black;
    [SerializeField] private float _dayLightIntensity = 1f;
    [SerializeField] private float _nightLightIntensity = 0.2f;
    [SerializeField] private float _transitionDuration = 2f;

    public int Level => _level;
    public float DayDuration => _dayDuration;
    public float NightDuration => _nightDuration;
    public Color DayLightColor => _dayLightColor;
    public Color NightLightColor => _nightLightColor;
    public float DayLightIntensity => _dayLightIntensity;
    public float NightLightIntensity => _nightLightIntensity;
    public float TransitionDuration => _transitionDuration;
}
