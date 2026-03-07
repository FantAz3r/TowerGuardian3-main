using System.Collections;
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
    [field: SerializeField] public PlayerHealth Health { get; private set; }
    [field: SerializeField] public PlayerMover PlayerMover { get; private set; }
    [field: SerializeField] public QuestPointer QuestPointer { get; private set; }
    [field: SerializeField] public Fist Fist { get; private set; }
    [field: SerializeField] public ResourceCollector ResourceCollector { get; private set; }
    [field: SerializeField] public HealthRegeneration HealthRegeneration { get; private set; }
    [field: SerializeField] public Mover Mover { get; private set; }
    [field: SerializeField] public Camera ModelViewCamera { get; private set; }

    private IGameConditionService _conditionService;
    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(5);
    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _conditionService = ServiceLocator.Get<IGameConditionService>();
        Health.Died += OnDied;
    }

    private void Start()
    {
        YG2.saves.PlayerPosition = transform.position;
        YG2.SaveProgress();
        StartCoroutine(SaveRoutine());
    }

    private IEnumerator SaveRoutine()
    {
        while(enabled)
        {
            YG2.saves.PlayerPosition = transform.position;
            YG2.SaveProgress();
            yield return _delay;
        }
    }

    private void OnDied()
    {
        _conditionService.OnLouse(Health.gameObject);
    }

    private void OnDestroy()
    {
        Health.Died -= OnDied;
        IsAlive = false;
    }
}
