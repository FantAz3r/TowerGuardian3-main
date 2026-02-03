using UnityEngine;

public class ResourcePiece : MonoBehaviour
{
    [SerializeField] private ResourceType _pieceType;
    [SerializeField] private AudioClip _pickSound;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private int _amount = 1;

    private ResourcePieceAnimator _animator;
    private ISpawnerService _spawnerService;

    public AudioClip CollectSound => _collectSound;
    public ResourceType PeiceType => _pieceType;
    public int Amount => _amount;

    private void Awake()
    {
        _animator = GetComponentInChildren<ResourcePieceAnimator>();
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
    }

    public void OnTake()
    {
        _animator.OnTake();
        _spawnerService.SendSoundReqest(_pickSound, transform.position);
    }
}
