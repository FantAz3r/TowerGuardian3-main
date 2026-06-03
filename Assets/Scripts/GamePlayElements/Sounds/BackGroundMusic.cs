using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musics;
    [SerializeField] private List<AudioClip> _battleSounds;
    [SerializeField] private float _delayAfterTrack = 1f;

    private List<AudioClip> _currentSounds = new ();
    private ISpawnerService _spawnerService;
    private ICoroutineRunner _coroutineRunner;
    private Coroutine _audioRoutine;
    private int _previousMusicIndex = -1;

    private void Awake()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
    }

    private void Start()
    {
        if (_musics == null || _musics.Count == 0) return;

        _currentSounds = _musics;
        StartNextTrack(ref _previousMusicIndex);
    }

    private void OnDisable()
    {
        StopAudioRoutine();
    }

    private void StartNextTrack(ref int previousIndex)
    {
        if (_currentSounds == null || _currentSounds.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, _currentSounds.Count);

        previousIndex = randomIndex;
        AudioClip clip = _currentSounds[randomIndex];

        _spawnerService.SendSoundReqest(clip);
        _audioRoutine = _coroutineRunner.StartCoroutine(PlayAndWait(clip));
    }

    private IEnumerator PlayAndWait(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length + _delayAfterTrack);
        StartNextTrack(ref _previousMusicIndex);
    }

    public void StartBattleMusic()
    {
        _spawnerService.ClearObjects(SpawnerType.Sounds);
        StopAudioRoutine();
        _currentSounds = _battleSounds;
        StartNextTrack(ref _previousMusicIndex);
    }

    private void StopAudioRoutine()
    {
        if (_audioRoutine != null && _coroutineRunner != null)
        {
            _coroutineRunner.StopCoroutine(_audioRoutine);
            _audioRoutine = null;
        }
    }
}
