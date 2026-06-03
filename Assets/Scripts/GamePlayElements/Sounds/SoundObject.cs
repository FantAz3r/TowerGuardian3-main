using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundObject : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audioClip;
    private Coroutine _coroutine;
    private float _currentPlayTime;
    private ISoundService _soundService;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _soundService = ServiceLocator.Get<ISoundService>();
    }

    public void PlayAndDisable(AudioClip clip)
    {
        _audioClip = clip;
        _currentPlayTime = 0;
        _audioSource.clip = _audioClip;
        _audioSource.Play();
        _coroutine = StartCoroutine(DisableAfterSeconds(clip.length));
    }

    private void OnDestroy()
    {
        _soundService.Remove(this);
    }

    private System.Collections.IEnumerator DisableAfterSeconds(float clipLength)
    {
        while (_currentPlayTime <= clipLength)
        {
            _currentPlayTime += Time.deltaTime;
            yield return null;
        }

        _soundService.Remove(this);
        gameObject.SetActive(false);
    }

    public void StopSound()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _audioSource.Pause();
    }

    public void ContinueSound()
    {
        _audioSource.UnPause();
        _coroutine = StartCoroutine(DisableAfterSeconds(_audioClip.length - _currentPlayTime));
    }
}