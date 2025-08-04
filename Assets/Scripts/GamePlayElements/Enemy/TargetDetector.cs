using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    private Player _target;
    private int _playerColliderCount = 0;

    public Player GetTarget() => _target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerColliderCount++;
            _target = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerColliderCount--;

            if (_playerColliderCount <= 0)
            {
                _playerColliderCount = 0;
                _target = null;  
            }
        }
    }
}
