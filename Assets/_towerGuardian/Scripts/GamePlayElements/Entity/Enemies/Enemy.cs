using TowerGuardian.StaticData;
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
    [field: SerializeField] public ThrownObjectDetector ThrownObjectDetector { get; private set; }
    [field: SerializeField] public PickUper PickUper { get; private set; }
    [field: SerializeField] public ProjectileSpawner ProjectileSpawner { get; private set; }
    [field: SerializeField] public ProjectileRotator ProjectileRotator { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }
    [field: SerializeField] public SphereCollider TargetDetectorCollider { get; private set; }
    [field: SerializeField] public Orbit Orbit { get; private set; }
    [field: SerializeField] public ForceField ForceField { get; private set; }

    public Transform Target { get; private set; }
    public Transform ThrownObject { get; private set; }

    public void Init(Transform player, int level = 0)
    {
        BehaviorAnimator.runtimeAnimatorController = Config.Controller;
        TargetDetectorCollider.radius = Config.DetectionRadius;

        Target = player;

        Config.SetLevel(level);

        Agent.SetMoveSpeed(Config.GetMoveSpeed());
        Agent.SetAngularSpeed(Config.MoveConfig.RotationSpeed);

        Health.Init(Config.HealthConfig.GetMaxHealth());
        StateMachine.Init();
    }

    public void SetNewTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    public void SetThrownObject(Transform thrownObject)
    {
        ThrownObject = thrownObject;
    }

    private void OnDisable()
    {
        Target = null;
    }
}
