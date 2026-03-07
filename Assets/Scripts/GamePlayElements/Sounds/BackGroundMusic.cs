using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musics; 
    [SerializeField] private float _delayBetweenTracks = 1f;

    private ISpawnerService _spawnerService;
    private ICoroutineRunner _coroutineRunner;
    private Coroutine _audioRoutine;
    private int _previousIndex = -1;

    private void Awake()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
    }

    private void Start()
    {
        if (_musics == null || _musics.Count == 0) return;

        StartNextTrack();
    }

    private void OnDisable()
    {
        if (_audioRoutine != null)
            _coroutineRunner.StopCoroutine(_audioRoutine);
    }

    private void StartNextTrack()
    {
        if (_musics.Count == 1)
        {
            Debug.Log("Необходимо 2 трека");
            return;
        }

        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, _musics.Count);
        }
        while (randomIndex == _previousIndex);

        _previousIndex = randomIndex;
        AudioClip clip = _musics[randomIndex];
        _spawnerService.SendSoundReqest(clip);
        _audioRoutine = _coroutineRunner.StartCoroutine(PlayAndWait(clip));
    }

    private IEnumerator PlayAndWait(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length + _delayBetweenTracks);
        StartNextTrack();
    }
}
