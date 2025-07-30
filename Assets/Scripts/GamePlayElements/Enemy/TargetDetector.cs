using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    private Player _target;

    public Player GetTarget() => _target;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Player player);
        _target = player;
    }

    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent(out Player player);
        _target = null;
    }
}
