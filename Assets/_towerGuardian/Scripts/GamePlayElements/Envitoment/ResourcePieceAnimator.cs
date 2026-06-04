using UnityEngine;

public class ResourcePieceAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _hashTake;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _hashTake = Animator.StringToHash("Get");
    }

    public void OnTake()
    {
        _animator.SetTrigger(_hashTake);
    }
}
