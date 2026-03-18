using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private float _attackRangeTreshold = 1.1f;
    private int _hashIsSeeTarget, _hashTargetInAttackRange, _hashRandom, _hashHasObject, _hashPick, _hashThrowEnded;

    private void Awake()
    {
        _hashIsSeeTarget = Animator.StringToHash("SeeTarget");
        _hashTargetInAttackRange = Animator.StringToHash("TargetInAttackRange");
        _hashRandom = Animator.StringToHash("Random");
        _hashHasObject = Animator.StringToHash("HasObject");
        _hashPick = Animator.StringToHash("IsPickup");
        _hashThrowEnded = Animator.StringToHash("ThrowEnded");
    }

    public void Init()
    {
        _enemy.Agent.EnableAgent(true);
        _enemy.Collider.enabled = true;
        _enemy.Rotator.CanRotate(true);
        
        _enemy.TargetDetector.TargetDetected += OnSeeTarget;
        _enemy.TargetDetector.TargetLost += OnLostTarget;

        _enemy.Health.Died += OnDie;
    }

    private void Update()
    {
        if(_enemy.Target == null) 
            return;

        float sqrDistance = (transform.position - _enemy.Target.transform.position).sqrMagnitude;

        if (sqrDistance <= _enemy.Config.AttackRange * _enemy.Config.AttackRange)
        {
            _enemy.BehaviorAnimator.SetBool(_hashTargetInAttackRange, true);
        }
        else if(sqrDistance >= _enemy.Config.AttackRange * _enemy.Config.AttackRange * _attackRangeTreshold)
        {
            _enemy.BehaviorAnimator.SetBool(_hashTargetInAttackRange, false);
        }

        if(sqrDistance <= _enemy.Config.DetectionRadius * _enemy.Config.DetectionRadius)
        {
            _enemy.BehaviorAnimator.SetBool(_hashIsSeeTarget, true);
        }
        else
        {
            _enemy.BehaviorAnimator.SetBool(_hashIsSeeTarget, false);
        }

    }

    private void OnSeeTarget(Transform target)
    {
        _enemy.SetNewTarget(target);
        SetRandom();
    }

    private void OnLostTarget(Transform target)
    {
        _enemy.SetNewTarget(target);
    }

    public void SetRandom(int random = -1)
    {
        if(random != -1)
        {
            _enemy.BehaviorAnimator.SetInteger(_hashRandom, random);
        }
        else
        {
            _enemy.BehaviorAnimator.SetInteger(_hashRandom, Random.Range(0, 4));
        }
    }

    public void OnDie()
    {
        _enemy.Collider.enabled = false;
        _enemy.Agent.EnableAgent(false);
        _enemy.Rotator.CanRotate(false);

        _enemy.Health.Died -= OnDie;
    }

    public void OnReadyToThrow()
    {
        _enemy.BehaviorAnimator.SetBool(_hashPick, false);
        _enemy.BehaviorAnimator.SetBool(_hashHasObject, true);
    }

    public void OnNullTrownObject()
    {
        _enemy.BehaviorAnimator.SetBool(_hashHasObject, false);
    }

    public void OnStartPickup()
    {
        _enemy.BehaviorAnimator.SetBool(_hashPick, true);
    }

    public void OnStopPickup()
    {
        _enemy.BehaviorAnimator.SetBool(_hashPick, false);
    }

    public void OnThrowEnded()
    {
        SetRandom();
        _enemy.BehaviorAnimator.SetTrigger(_hashThrowEnded);
    }
}