using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSpawner : BaseSpawner
{
    private Dictionary<AudioMixerGroup, ObjectPool<SoundObject>> _pools = new ();
    private SoundData _soundData;
    private WaitForSecondsRealtime _delay;
    private ICoroutineRunner _coroutineRunner;
    private ISoundService _soundService;
    private HashSet<AudioClip> _blockedClips = new ();
    private float _minDelayBetweenSameClip = 0.15f;

    public SoundSpawner(SoundData data, SoundObject prefab)
    {
        _soundData = data;
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
        _soundService = ServiceLocator.Get<ISoundService>();
        foreach (var info in data.SoundInfos)
        {
            if (_pools.ContainsKey(info.AudioGroup) == false)
            {
                _pools.Add(info.AudioGroup, new ObjectPool<SoundObject>(prefab, 0, true));
            }
        }

        _delay = new WaitForSecondsRealtime(_minDelayBetweenSameClip);
    }

    public override SpawnerType GetSpawnerType() => SpawnerType.Sounds;

    public override void DestroyPool()
    {
        foreach (var pair in _pools)
        {
            pair.Value.DestroyPool();
        }
    }

    public override void ClearObjects()
    {
        foreach (var pair in _pools)
        {
            pair.Value.Clear();
        }
    }

    public override void Spawn(AudioClip clip, Vector3 position)
    {
        if (CanSpawn == false) return;
        if (clip == null) return;
        if (_blockedClips.Contains(clip)) return;

        AudioMixerGroup soundGroup = GetClipType(clip);

        if (_pools.TryGetValue(soundGroup, out var pool) == false) return;

        SoundObject soundObject = pool.Get();

        soundObject.transform.position = position;
        soundObject.TryGetComponent(out AudioSource audioSource);
        audioSource.outputAudioMixerGroup = soundGroup;

        if (soundGroup.name == SoundType.UI.ToString())
        {
            audioSource.spatialBlend = 0f;
        }

        soundObject.PlayAndDisable(clip);
        _blockedClips.Add(clip);
        _soundService.Add(soundObject);
        _coroutineRunner.StartCoroutine(UnblockClipAfterDelay(clip));
    }

    private IEnumerator UnblockClipAfterDelay(AudioClip clip)
    {
        yield return _delay;
        _blockedClips.Remove(clip);
    }

    private AudioMixerGroup GetClipType(AudioClip clip)
    {
        foreach (var info in _soundData.SoundInfos)
        {
            if (clip == info.AudioClip)
            {
                return info.AudioGroup;
            }
        }

        return null;
    }
}
