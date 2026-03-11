using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private int _hashIsSeePlayer, _hashIsPlayerInAttackRange, _hashRandom, _hashHasObject, _hashPick, _hashThrowEnded;

    private void Awake()
    {
        _hashIsSeePlayer = Animator.StringToHash("SeePlayer");
        _hashIsPlayerInAttackRange = Animator.StringToHash("PlayerInAttackRange");
        _hashRandom = Animator.StringToHash("Random");
        _hashHasObject = Animator.StringToHash("HasObject");
        _hashPick = Animator.StringToHash("IsPickup");
        _hashThrowEnded = Animator.StringToHash("ThrowEnded");
    }

    public void Init()
    {
        _enemy.Agent.EnableAgent(true);
        _enemy.Collider.enabled = true;

        _enemy.TargetDetector.PlayerDetected += OnSeePlayer;
        _enemy.TargetDetector.PlayerLost += OnLostPlayer;

        _enemy.AttackDetector.PlayerDetected += OnPlayerInMeleeRange;
        _enemy.AttackDetector.PlayerLost += OnChasePlayer;

        _enemy.Health.Died += OnDie;
    }

    private void OnSeePlayer()
    {
        SetRandom();
        _enemy.BehaviorAnimator.SetBool(_hashIsSeePlayer, true);
    }

    public void SetRandom(int random = -1)
    {
        if(random != -1)
        {
            _enemy.BehaviorAnimator.SetInteger(_hashRandom, random);
        }
        else
        {
            _enemy.BehaviorAnimator.SetInteger(_hashRandom, Random.Range(0, 2));
        }
    }

    public void OnDie()
    {
        _enemy.Collider.enabled = false;
        _enemy.Agent.EnableAgent(false);

        _enemy.TargetDetector.PlayerDetected -= OnSeePlayer;
        _enemy.TargetDetector.PlayerLost -= OnLostPlayer;
        _enemy.AttackDetector.PlayerDetected -= OnPlayerInMeleeRange;
        _enemy.AttackDetector.PlayerLost -= OnChasePlayer;
        _enemy.Health.Died -= OnDie;
    }

    public void OnLostPlayer()
    {
        _enemy.BehaviorAnimator.SetBool(_hashIsSeePlayer, false);
        _enemy.BehaviorAnimator.SetBool(_hashIsPlayerInAttackRange, false);
    }

    private void OnPlayerInMeleeRange()
    {
        _enemy.BehaviorAnimator.SetBool(_hashIsSeePlayer, true);
        _enemy.BehaviorAnimator.SetBool(_hashIsPlayerInAttackRange, true);
    }

    public void OnChasePlayer()
    {
        _enemy.BehaviorAnimator.SetBool(_hashIsPlayerInAttackRange, false);
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
        _enemy.BehaviorAnimator.SetTrigger(_hashThrowEnded);
    }
}