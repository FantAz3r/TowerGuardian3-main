using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetPosition = new Vector3(0, 10, -10);
    [SerializeField] private Vector3 _rotation = new Vector3(40, 0, 0);

    private Transform _target;
    private Vector3 _currentPosition;

    public void Init(Transform target)
    {
        _target = target;
        transform.rotation = Quaternion.Euler(_rotation);
    }

    private void LateUpdate()
    {
        transform.position = _target.position + _offsetPosition;
        _currentPosition = _target.position;
    }

    public void ChangeTarget()
    {
        _target.position = _currentPosition;
    }
}