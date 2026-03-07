using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveViewer : WindowBase
{
    [SerializeField] private Slider _waveSlider;
    [SerializeField] private RectTransform _flagsContainer;
    [SerializeField] private GameObject _flagPrefab;

    private float _elapsedTime = 0f;
    private List<Wave> _waves;
    private List<float> _waveDurationsAccumulated = new List<float>();
    private float _totalDuration;
    private QuestStateMachine _questRunner;
    private ICoroutineRunner _coroutineRunner;
    private IGameFactory _gameFactory;
    private Coroutine _waveRoutine;

    private void Awake()
    {
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
        _gameFactory = ServiceLocator.Get<IGameFactory>();

        _waves = _gameFactory.LevelConfig.Waves;
        _questRunner = _gameFactory.QuestRunner;

        if (_waves == null || _waves.Count == 0)
            return;

        _totalDuration = 0f;
        _waveDurationsAccumulated.Clear();

        for (int i = 0; i < _waves.Count - 1; i++)
        {
            _totalDuration += _waves[i].Duration;
            _waveDurationsAccumulated.Add(_totalDuration);
        }

        _waveSlider.minValue = 0;
        _waveSlider.maxValue = _totalDuration;
        _waveSlider.value = 0;

        DrawFlags();
    }

    private void OnDisable()
    {
        if (_waveRoutine != null)
            _coroutineRunner.StopCoroutine(_waveRoutine);
    }

    private void DrawFlags()
    {
        foreach (Transform child in _flagsContainer)
        {
            Destroy(child.gameObject);
        }

        float containerWidth = _flagsContainer.rect.width;

        for (int i = 0; i < _waves.Count - 1; i++)
        {
            var flag = Instantiate(_flagPrefab, _flagsContainer);
            var layoutElement = flag.GetComponent<LayoutElement>();
            layoutElement.flexibleWidth = _waves[i].Duration;

            if (i == 0)
            {
                Image image = flag.GetComponent<Image>();
                image.enabled = false;
            }
        }

        _waveRoutine = _coroutineRunner.StartCoroutine(WaitForSliderToMax());
    }

    private IEnumerator WaitForSliderToMax()
    {
        if (_waveSlider == null) yield break;

        while (_waveSlider.value < _totalDuration)
        {
            _elapsedTime += Time.deltaTime;
            _elapsedTime = Mathf.Clamp(_elapsedTime, 0, _totalDuration);
            _waveSlider.value = _elapsedTime;

            yield return null;
        }

        _questRunner.SetQuest(QuestType.GetOut);
    }
}
