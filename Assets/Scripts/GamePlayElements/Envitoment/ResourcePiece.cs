using UnityEngine;

public class ResourcePiece : MonoBehaviour
{
    [SerializeField] private ResourceType _pieceType;
    [SerializeField] private AudioClip _pickSound;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private int _amount = 1;
    [SerializeField] private Renderer _outlineRenderer;
    [field: SerializeField] public int ScorePoints { get; private set; } = 1;

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

    public void SetAmount(int amount)
    {
        _amount = amount;
        UpdateOutlineColor();
    }

    private void UpdateOutlineColor()
    {
        if (_outlineRenderer == null) return;

        Color color;

        if (_amount >= 1 && _amount <= 4)
            color = Color.white;
        else if (_amount >= 5 && _amount <= 7)
            color = Color.green;
        else if (_amount >= 8 && _amount <= 11)
            color = Color.blue;
        else if (_amount >= 12 && _amount <=16)
            color = Color.red;
        else if (_amount > 16)
            color = Color.yellow;
        else
            color = Color.white;

        _outlineRenderer.material.SetColor("_OutlineColor", color);
    }

    public void OnTake()
    {
        _animator.OnTake();
        _spawnerService.SendSoundReqest(_pickSound, transform.position);
    }
}
