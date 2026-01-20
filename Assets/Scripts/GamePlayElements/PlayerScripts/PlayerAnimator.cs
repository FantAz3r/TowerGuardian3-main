using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private float _smoothTime = 0.05f;

    [Header("Параметры поиска клипа атаки")]
    [Tooltip("Имя клипа атаки внутри AnimatorController. Если пусто, будет попытка взять первый попавшийся клип.")]
    [SerializeField] private string attackClipName = "Attack";

    private Animator _animator;
    private Mover _mover;
    private Rotator _rotator;
    private PlayerAttacker _attacker;
    private Health _health;
    private int _hashX;
    private int _hashY;
    private int _hashWeaponSeted;
    private int _hashWeaponRemoved;
    private int _hashAttack;
    private int _hashRandom;
    private int _hasWeapon;
    private int _hashDie;

    private float _currentSpeed;
    private float _velSpeed;

    private Coroutine _resetSpeedCoroutine;
    private float _defaultAnimatorSpeed = 1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover>();
        _rotator = GetComponentInChildren<Rotator>();
        _attacker = GetComponentInChildren<PlayerAttacker>();
        _health = GetComponent<Health>();

        _hashX = Animator.StringToHash("X");
        _hashY = Animator.StringToHash("Y");
        _hashWeaponSeted = Animator.StringToHash("WeaponSeted");
        _hashWeaponRemoved = Animator.StringToHash("WeaponRemoved");
        _hashAttack = Animator.StringToHash("Attack");
        _hashRandom = Animator.StringToHash("Random");
        _hasWeapon = Animator.StringToHash("HasWeapon");
        _hashDie = Animator.StringToHash("Die");

        if (_animator != null)
            _defaultAnimatorSpeed = _animator.speed;
    }

    private void OnEnable()
    {
        if (_attacker == null)
            return;

        _attacker.WeaponSeted += OnWeaponSeted;
        _attacker.WeaponRemoved += OnWeaponRemoved;
        _attacker.Attacked += PlayAttack;
        _attacker.Suspended += OnSuspendAttack;
        _health.Died += OnDie;
    }

    private void OnDisable()
    {
        if (_attacker == null)
            return;

        _attacker.WeaponSeted -= OnWeaponSeted;
        _attacker.WeaponRemoved -= OnWeaponRemoved;
        _attacker.Attacked -= PlayAttack;
        _health.Died -= OnDie;
    }

    private void Update()
    {
        UpdateMovementParameters();
    }

    private void UpdateMovementParameters()
    {
        float x = 0f;
        float y = 0f;
        float dampTime = 0.05f;
        float trashhold = 0.001f;

        Vector2 lookDirection = _rotator.CurrentDirection.normalized;
        Vector2 moveDirection = _mover.Direction.normalized;

        float moveSpeed = _mover.Direction.SqrMagnitude();

        if (moveSpeed > trashhold)
        {
            float angleDifference = Vector2.SignedAngle(lookDirection, moveDirection) * Mathf.Deg2Rad;
            x = Mathf.Sin(angleDifference);
            y = Mathf.Cos(angleDifference);
        }


        float targetSpeed = moveSpeed * _speedMultiplier;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _velSpeed, _smoothTime);

        _animator.SetFloat(_hashX, x, dampTime, Time.deltaTime);
        _animator.SetFloat(_hashY, y, dampTime, Time.deltaTime);
    }


    private void OnWeaponSeted(IWeapon weapon)
    {
        if (weapon.Config.Controller == _attacker.PreviousWeapon.Config.Controller)
        {
            SetParametrs();
        }
        else
        {
            if (_attacker.PreviousWeapon.Config.WeaponType == WeaponType.None)
            {
                _animator.runtimeAnimatorController = weapon.Config.Controller;
                SetParametrs();
            }
            else if (weapon.Config.WeaponType == WeaponType.None)
            {

                return;
            }
        }
    }

    public void SwapController()
    {
        _animator.runtimeAnimatorController = _attacker.CurrentWeapon.Config.Controller;
        _animator.speed = _defaultAnimatorSpeed;
    }

    public void OnWeaponRemoved(IWeapon weapon)
    {
        if (weapon.Config.WeaponType != WeaponType.None)
        {
            _animator.SetTrigger(_hashWeaponRemoved);
        }
    }

    public void OnAnimationEquipWeapon()
    {
        _animator.SetBool(_hasWeapon, true);
    }

    public void OnAnimationUnequipWeapon()
    {
        _animator.SetBool(_hasWeapon, false);
    }

    public void PlayAttack(IWeapon weapon, float attackDelay)
    {
        if (_animator == null)
            return;

        if (weapon.Config.WeaponType == WeaponType.None)
        {
            int random = Random.Range(0, 2);
            _animator.SetInteger(_hashRandom, random);
        }

        float clipLength = GetAttackClipLength();
        float desiredDuration = Mathf.Max(0.0001f, attackDelay);
        float requiredSpeed = clipLength / desiredDuration;

        if (_resetSpeedCoroutine != null)
            StopCoroutine(_resetSpeedCoroutine);

        _animator.speed = requiredSpeed;
        _animator.SetBool(_hashAttack, true);
        _resetSpeedCoroutine = StartCoroutine(ResetAnimatorSpeedAfter(desiredDuration));
    }

    private void OnSuspendAttack()
    {
        _animator.SetBool(_hashAttack, false);
    }

    private void SetParametrs()
    {
        _animator.SetTrigger(_hashWeaponSeted);
    }

    private IEnumerator ResetAnimatorSpeedAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _animator.speed = _defaultAnimatorSpeed;
        _resetSpeedCoroutine = null;
    }

    private float GetAttackClipLength()
    {
        if (_animator == null)
            return _defaultAnimatorSpeed;

        var controller = _animator.runtimeAnimatorController;
        if (controller == null)
            return _defaultAnimatorSpeed;

        AnimationClip[] clips = controller.animationClips;

        if (clips == null || clips.Length == 0)
            return _defaultAnimatorSpeed;

        if (string.IsNullOrEmpty(attackClipName) == false)
        {
            foreach (var clip in clips)
            {
                if (clip != null && clip.name == attackClipName)
                    return clip.length;
            }
        }

        foreach (var clip in clips)
        {
            if (clip != null && clip.name.ToLower().Contains("Attack"))
                return clip.length;
        }

        return _defaultAnimatorSpeed;
    }

    public void OnDie()
    {
        _animator.SetTrigger(_hashDie);
    }

    public void OnAnimationDie()
    {
        _health.Die();
    }

    public void OnAnimationAttack()
    {
        _attacker.OnAnimationAttack();
    }

    public void OnEquipWeapon()
    {
        _attacker.OnEquipWeapon();
    }

    public void OnTakeOffWeapon()
    {
        _attacker.OnTakeOffWeapon();
    }
}