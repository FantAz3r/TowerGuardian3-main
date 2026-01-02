
using System.Collections;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    private Transform _player;
    private Vector3 _target;
    private Tutorial _tutorial;
    private Coroutine _pointerRoutine;

    public void Init(Transform player, Tutorial tutorial)
    {
        _player = player;
        _tutorial = tutorial;

        _tutorial.QuestSeted += OnQuestSeted;
        _tutorial.Complited += OnQuestCompleted;
        _tutorial.CompliteWithoutLust += OnQuestCompleted;
    }

    private void OnDestroy()
    {
        _tutorial.QuestSeted -= OnQuestSeted;
        _tutorial.Complited -= OnQuestCompleted;
        _tutorial.CompliteWithoutLust -= OnQuestCompleted;
        StopPointerRoutine();
    }

    private void OnQuestSeted(IQuest quest)
    {
        _target = quest.TryGetTarget();

        if (_target != null)
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
        float offsetY = 0.3f;

        while (_target != Vector3.zero)
        {
            Vector3 direction = _target - _player.position;
            direction.y = 0f;

            if (direction.sqrMagnitude >= 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(direction);

                Vector3 offsetFlat = direction.normalized;
                transform.position = _player.position + offsetFlat + Vector3.up * offsetY;
            }

            yield return null; 
        }

        gameObject.SetActive(false);
        _pointerRoutine = null;
    }
}
