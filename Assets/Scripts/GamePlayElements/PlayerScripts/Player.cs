using UnityEngine;
using YG;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerAttacker Attacker { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }
    [field: SerializeField] public AttackZone AttackZone { get; private set; }
    [field: SerializeField] public PlayerExperience Experience { get; private set; }
    [field: SerializeField] public PlayerCardConfigContainer CardHolder { get; private set; }
    [field: SerializeField] public AllAbilities AllAbilities { get; private set; }
    [field: SerializeField] public EnemyDetector Detector { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public PlayerMover PlayerMover { get; private set; }
    [field: SerializeField] public QuestPointer QuestPointer { get; private set; }
    [field: SerializeField] public Fist Fist { get; private set; }
    [field: SerializeField] public ResourceCollector ResourceCollector { get; private set; }
    [field: SerializeField] public HealthRegeneration HealthRegeneration { get; private set; }
    [field: SerializeField] public Mover Mover { get; private set; }

    private IGameConditionService _conditionService;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _conditionService = ServiceLocator.Get<IGameConditionService>();
        Health.Died += OnDied;
    }

    private void OnDied()
    {
        _conditionService.OnLouse(Health.gameObject);
    }

    private void OnDestroy()
    {
        Health.Died -= OnDied;
        IsAlive = false;
        YG2.saves.PlayerPosition = transform.position;
        YG2.SaveProgress();
    }
}
