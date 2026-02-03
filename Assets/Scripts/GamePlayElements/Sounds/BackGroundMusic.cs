using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musics; 
    [SerializeField] private float _delayBetweenTracks = 1f;

    private ISpawnerService _spawnerService;

    private void Awake()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
    }

    private void Start()
    {
        if (_musics == null || _musics.Count == 0) return;

        StartCoroutine(PlayMusicSequence());
    }

    private IEnumerator PlayMusicSequence()
    {
        int previousIndex = -1;

        while (enabled)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, _musics.Count);
            }
            while (randomIndex == previousIndex && _musics.Count > 1);

            previousIndex = randomIndex;
            AudioClip clip = _musics[randomIndex];
            _spawnerService.SendSoundReqest(clip);

            yield return new WaitForSeconds(clip.length + _delayBetweenTracks);
        }
    }
}
