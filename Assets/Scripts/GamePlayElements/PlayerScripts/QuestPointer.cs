using DG.Tweening;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    private Vector3 _target;
    private QuestStateMachine _questRunner;
    private Player _player;
    private Tween _jumpTween;

    private float _arrowMoveSpeed = 8f;
    private float _jumpHeight = 0.5f;
    private float _jumpDuration = 0.5f;
    private float _jumpStartDistance = 10f;

    [SerializeField] private Vector3 _jumpOffset = new Vector3(0, 6f, 0);
    [SerializeField] private Vector3 _playerOffset = new Vector3(0, 3f, 0);

    private bool _isOverTarget = false;
    private bool _isMoving = false;

    public void Init()
    {
        _player = ServiceLocator.Get<IGameFactory>().Player;
        _questRunner = ServiceLocator.Get<IGameFactory>().QuestRunner;
        _questRunner.QuestStarted += OnQuestSeted;
        _questRunner.QuestCompleted += OnQuestCompleted;
    }

    private void OnDestroy()
    {
        if (_questRunner != null)
        {
            _questRunner.QuestStarted -= OnQuestSeted;
            _questRunner.QuestCompleted -= OnQuestCompleted;
        }

        StopJump();
    }

    private void Update()
    {
        if (enabled == false || _target == Vector3.zero || _player.transform == null)
            return;

        float distanceToTarget = Vector3.SqrMagnitude(_player.transform.position - _target);

        if (distanceToTarget >= _jumpStartDistance * _jumpStartDistance)
        {
            if (_isOverTarget)
            {
                transform.SetParent(_player.transform);
                StopJump();
                _isOverTarget = false;
            }

            Vector3 abovePlayer = _player.transform.position + _playerOffset;
            transform.position = Vector3.Lerp(transform.position, abovePlayer, Time.deltaTime * _arrowMoveSpeed);

            Vector3 direction = (_target - transform.position).normalized;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _arrowMoveSpeed);
            }
        }
        else
        {
            if (_isOverTarget == false)
            {
                transform.position = _player.transform.position + _jumpOffset;
                transform.SetParent(null);
                _isOverTarget = true;
                MoveArrow();
            }
        }
    }

    private void OnQuestSeted(IQuest quest)
    {
        StopJump();
        transform.SetParent(_player.transform);

        if (quest == null)
        {
            _target = Vector3.zero;
            gameObject.SetActive(false);
            return;
        }

        _target = quest.TryGetTarget();

        if (_target != Vector3.zero && _player.transform != null)
        {
            gameObject.SetActive(true);
            transform.localPosition = _jumpOffset;
            transform.localRotation = Quaternion.identity;
            _isOverTarget = false;
        }
        else
        {
            _target = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnQuestCompleted()
    {
        _target = Vector3.zero;
        gameObject.SetActive(false);
        StopJump();
    }

    void MoveArrow()
    {
        if (_isMoving) return; 

        Vector3 targetPos = _target + _jumpOffset;

        _isMoving = true;
        transform.DOMove(new Vector3(targetPos.x, transform.position.y, targetPos.z), _jumpDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _isMoving = false);

        transform.rotation = Quaternion.Euler(0, 180f, 180);
        StartJump();
    }


    private void StartJump()
    {
        if (_jumpTween != null && _jumpTween.IsActive()) return;

        _jumpTween = transform.DOLocalMoveY(transform.localPosition.y + _jumpHeight, _jumpDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void StopJump()
    {
        if (_jumpTween != null)
        {
            _jumpTween.Kill();
            _jumpTween = null;
        }
    }
}
