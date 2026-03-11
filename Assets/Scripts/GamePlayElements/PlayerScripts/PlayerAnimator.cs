using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("Настройки")]
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private float _smoothTime = 0.05f;

    [Header("Параметры поиска клипа атаки")]
    [Tooltip("Имя клипа атаки внутри AnimatorController. Если пусто, будет попытка взять первый попавшийся клип.")]
    [SerializeField] private string attackClipName = "Attack";
    
    private int _hashX;
    private int _hashY;
    private int _hashWeaponSeted;
    private int _hashWeaponRemoved;
    private int _hashAttack;
    private int _hashRandom;
    private int _hasWeapon;
    private int _hashDie;
    private int _hashRevive;

    private float _currentSpeed;
    private float _velSpeed;

    private IInputService _inputService;
    private IGameConditionService _gameConditionService;
    private Coroutine _resetSpeedCoroutine;
    private float _defaultAnimatorSpeed = 1f;

    private void Awake()
    {
        _inputService = ServiceLocator.Get<IInputService>();
        _gameConditionService = ServiceLocator.Get<IGameConditionService>();
        _inputService.EnableInput();

        _hashX = Animator.StringToHash("X");
        _hashY = Animator.StringToHash("Y");
        _hashWeaponSeted = Animator.StringToHash("WeaponSeted");
        _hashWeaponRemoved = Animator.StringToHash("WeaponRemoved");
        _hashAttack = Animator.StringToHash("Attack");
        _hashRandom = Animator.StringToHash("Random");
        _hasWeapon = Animator.StringToHash("HasWeapon");
        _hashDie = Animator.StringToHash("Died");
        _hashRevive = Animator.StringToHash("Revive");

        if (_player.Animator != null)
            _defaultAnimatorSpeed = _player.Animator.speed;
    }

    private void OnEnable()
    {
        if (_player.Attacker == null)
            return;

        _player.Attacker.WeaponSeted += OnWeaponSeted;
        _player.Attacker.WeaponRemoved += OnWeaponRemoved;
        _player.Attacker.Attacked += PlayAttack;
        _player.Attacker.Suspended += OnSuspendAttack;
        _player.Health.Died += OnDie;
    }

    private void OnDisable()
    {
        if (_player.Attacker == null)
            return;

        _player.Attacker.WeaponSeted -= OnWeaponSeted;
        _player.Attacker.WeaponRemoved -= OnWeaponRemoved;
        _player.Attacker.Attacked -= PlayAttack;
        _player.Health.Died -= OnDie;
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

        Vector2 lookDirection = _player.Rotator.CurrentDirection.normalized;
        Vector2 moveDirection = _player.Mover.Direction.normalized;

        float moveSpeed = _player.Mover.Direction.SqrMagnitude();

        if (moveSpeed > trashhold)
        {
            float angleDifference = Vector2.SignedAngle(lookDirection, moveDirection) * Mathf.Deg2Rad;
            x = Mathf.Sin(angleDifference);
            y = Mathf.Cos(angleDifference);
        }


        float targetSpeed = moveSpeed * _speedMultiplier;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _velSpeed, _smoothTime);

        _player.Animator.SetFloat(_hashX, x, dampTime, Time.deltaTime);
        _player.Animator.SetFloat(_hashY, y, dampTime, Time.deltaTime);
    }

    private void OnWeaponSeted(IWeapon weapon)
    {
        if (weapon.Config.Controller == _player.Attacker.PreviousWeapon.Config.Controller)
        {
            SetParametrs();
        }
        else
        {
            if (_player.Attacker.PreviousWeapon.Config.WeaponType == WeaponType.None)
            {
                _player.Animator.runtimeAnimatorController = weapon.Config.Controller;
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
        _player.Animator.runtimeAnimatorController = _player.Attacker.CurrentWeapon.Config.Controller;
        _player.Animator.speed = _defaultAnimatorSpeed;
    }

    public void OnWeaponRemoved(IWeapon weapon)
    {
        if (weapon.Config.WeaponType != WeaponType.None)
        {
            _player.Animator.SetTrigger(_hashWeaponRemoved);
        }
    }

    public void OnAnimationEquipWeapon()
    {
        _player.Animator.SetBool(_hasWeapon, true);
    }

    public void OnAnimationUnequipWeapon()
    {
        _player.Animator.SetBool(_hasWeapon, false);
    }

    public void OnEquipWeapon()
    {
        _player.Attacker.OnEquipWeapon();
    }

    public void OnTakeOffWeapon()
    {
        _player.Attacker.OnTakeOffWeapon();
    }

    public void PlayAttack(IWeapon weapon, float attackDelay)
    {
        if (_player.Animator == null)
            return;

        if (weapon.Config.WeaponType == WeaponType.None)
        {
            int random = Random.Range(0, 2);
            _player.Animator.SetInteger(_hashRandom, random);
        }

        float clipLength = GetAttackClipLength();
        float desiredDuration = Mathf.Max(0.0001f, attackDelay);
        float requiredSpeed = clipLength / desiredDuration;

        if (_resetSpeedCoroutine != null)
            StopCoroutine(_resetSpeedCoroutine);

        _player.Animator.speed = requiredSpeed;
        _player.Animator.SetBool(_hashAttack, true);
        _resetSpeedCoroutine = StartCoroutine(ResetAnimatorSpeedAfter(desiredDuration));
    }

    private void OnSuspendAttack()
    {
        _player.Animator.SetBool(_hashAttack, false);
    }

    private void SetParametrs()
    {
        _player.Animator.SetTrigger(_hashWeaponSeted);
    }

    private IEnumerator ResetAnimatorSpeedAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _player.Animator.speed = _defaultAnimatorSpeed;
        _resetSpeedCoroutine = null;
    }

    private float GetAttackClipLength()
    {
        if (_player.Animator == null)
            return _defaultAnimatorSpeed;

        var controller = _player.Animator.runtimeAnimatorController;
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
        _inputService.DisableInput();
        _player.Animator.SetTrigger(_hashDie);
        _player.Health.Died -= OnDie;
    }

    public void OnAnimationDie()
    {
        _gameConditionService.OnLouse(_player.Health.gameObject);
    }

    public void OnAnimationAttack()
    {
        _player.Attacker.OnAnimationAttack();
    }

    public void OnRevive()
    {
        _player.Animator.SetTrigger(_hashRevive);
        _player.Health.Died += OnDie;
    }
}