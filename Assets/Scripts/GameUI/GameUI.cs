using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UIDummy _settings;
    [field: SerializeField] public Shop Shop { get; private set; }
    [field: SerializeField] public Sell Sell { get; private set; }
    [field: SerializeField] public LouseLevelMenu LouseLevelMenu { get; private set; }
    [field: SerializeField] public ScoreViewer LouseScoreViewer { get; private set; }
    [field: SerializeField] public StartLevelMenu StartLevelMenu { get; private set; }
    [field: SerializeField] public ScoreViewer StartScoreViewer { get; private set; }
    [field: SerializeField] public WinLevelMenu WinLevelMenu { get; private set; }
    [field: SerializeField] public ScoreViewer WinScoreViewer { get; private set; }
    [field: SerializeField] public AbilityPanel AbilityPanel { get; private set; }
    [field: SerializeField] public QuestViewer QuestViewer { get; private set; }
    [field: SerializeField] public ResourceViewer ResourceViewer { get; private set; }
    [field: SerializeField] public PlayerHealthViewer PlayerHealthViewer { get; private set; }
    [field: SerializeField] public LevelViewer LevelViewer { get; private set; }
    [field: SerializeField] public PauseUI PauseUI { get; private set; }
    [field: SerializeField] public SwichDamageNumbers SwichDamageNumbers { get; private set; }
    [field: SerializeField] public Mute Mute { get; private set; }
    [field: SerializeField] public CardSelectionMenu CardSelectionMenu { get; private set; }
    [field: SerializeField] public WeaponPanel WeaponPanel { get; private set; }
    [field: SerializeField] public Clock Clock { get; private set; }
    [field: SerializeField] public UIDummy HUD { get; private set; }
       
    private void Awake()
    {
        _settings.gameObject.SetActive(false);
    }
}
