using UnityEngine;

public class ResourcePiece : MonoBehaviour
{
    [SerializeField] private ResourceType _pieceType;
    [SerializeField] private int _amount = 1;

    public ResourceType PeiceType => _pieceType;
    public int Amount => _amount;
}
