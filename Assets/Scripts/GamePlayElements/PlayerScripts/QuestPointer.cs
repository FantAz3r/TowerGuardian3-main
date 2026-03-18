
using System.Collections;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    private Vector3 _target;
    private QuestStateMachine _questRunner;
    private Coroutine _pointerRoutine;
    private IGameFactory _gameFactory;

    public void Init()
    {
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

        StopPointerRoutine();
    }

    private void OnQuestSeted(IQuest quest)
    {
        if (quest == null)
        {
            StopPointerRoutine();
            gameObject.SetActive(false);
            return;
        }

        _target = quest.TryGetTarget();

        if (_target != Vector3.zero)
        {
            gameObject.SetActive(true);
            StartPointerRoutine();
        }
        else
        {
            StopPointerRoutine();
            gameObject.SetActive(false);
        }
    }

    private void OnQuestCompleted()
    {
        _target = Vector3.zero;
        StopPointerRoutine();
        gameObject.SetActive(false);
    }

    private void StartPointerRoutine()
    {
        if (_pointerRoutine != null)
            return;

        _pointerRoutine = StartCoroutine(PointerCoroutine());
    }

    private void StopPointerRoutine()
    {
        if (_pointerRoutine != null)
        {
            StopCoroutine(_pointerRoutine);
            _pointerRoutine = null;
        }
    }

    private IEnumerator PointerCoroutine()
    {
        while (_target != Vector3.zero)
        {
            Vector3 direction = (_target - transform.position).normalized;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            yield return null;
        }

        gameObject.SetActive(false);
        _pointerRoutine = null;
    }
}
