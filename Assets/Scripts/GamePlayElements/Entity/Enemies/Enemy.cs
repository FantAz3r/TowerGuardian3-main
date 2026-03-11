using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyConfig Config { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Rotator Rotator { get; private set; }
    [field: SerializeField] public AttackZone AttackZone { get; private set; }
    [field: SerializeField] public EnemyAnimator AnimationAnimator { get; private set; }
    [field: SerializeField] public Animator BehaviorAnimator { get; private set; }
    [field: SerializeField] public EnemyStateMachine StateMachine { get; private set; }
    [field: SerializeField] public EnemyMover Agent { get; private set; }
    [field: SerializeField] public TargetDetector TargetDetector { get; private set; }
    [field: SerializeField] public AttackDetector AttackDetector { get; private set; }
    [field: SerializeField] public ThrownObjectDetector ThrownObjectDetector { get; private set; }
    [field: SerializeField] public PickUper PickUper { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }

    public Transform Target { get; private set; }
    public Transform ThrownObject { get; private set; }


    public void Init(Transform player, int level)
    {
        BehaviorAnimator.runtimeAnimatorController = Config.Controller;
        Target = player;

        Config.SetLevel(level);

        Agent.SetMoveSpeed(Config.GetMoveSpeed());
        Agent.SetAngularSpeed(Config.MoveConfig.RotationSpeed);

        Health.Init(Config.GetMaxHealth());
        StateMachine.Init();
    }

    public void SetThrownObject(Transform thrownObject)
    {
        ThrownObject = thrownObject;
    }
}
