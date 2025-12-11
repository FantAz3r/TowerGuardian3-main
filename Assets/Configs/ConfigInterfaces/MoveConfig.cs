using UnityEngine;

public abstract class MoveConfig : HealthConfig, IMoveConfig
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 1f;

    public float MoveSpeed => _moveSpeed;
    public float RotationSpeed => _rotationSpeed;
}
