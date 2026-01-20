using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundObject : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAndDisable(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
        StartCoroutine(DisableAfterSeconds(clip.length));
    }

    private System.Collections.IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        gameObject.SetActive(false);
    }
}
