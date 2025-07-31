using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private ScriptableObject _configObject;
    private IMoveConfig _config;

    public Vector2 MoveDirection { get; private set; }

    public void SetDirection(Vector2 direction) => MoveDirection = direction;

    private void Awake()
    {
        _config = _configObject as IMoveConfig;
        if (_config == null)
            throw new ArgumentNullException();
    }

    private void Update()
    {
        Rotate(MoveDirection);
    }

    public void Rotate(Vector2 direction)
    {

        if (direction.sqrMagnitude < 0.001f)
        {
            direction = Vector2.zero;
            return;
        }

        Vector3 targetDirection3D = new Vector3(direction.x, 0f, direction.y).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection3D);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _config.RotationSpeed * Time.deltaTime);
    }
}
