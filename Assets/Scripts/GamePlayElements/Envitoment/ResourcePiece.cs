using UnityEngine;

public class ResourcePiece : MonoBehaviour
{
    [SerializeField] private ResourceType _pieceType;
    [SerializeField] private int _amount = 1;

    private ResourcePieceAnimator _animator;
    public ResourceType PeiceType => _pieceType;
    public int Amount => _amount;

    private void Awake()
    {
        _animator = GetComponentInChildren<ResourcePieceAnimator>();
    }

    public void OnTake()
    {
        _animator.OnTake();
    }
}
