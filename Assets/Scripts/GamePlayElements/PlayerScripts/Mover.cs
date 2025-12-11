using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Mover : MonoBehaviour
{
    [SerializeField] private MoveConfig _configObject;
    private float _moveSpeed;
    private float _startSpeed;
    public Vector2 Direction { get; private set; }

    public void SetDirection(Vector2 direction) => Direction = direction;

    private void Awake()
    {
        if (_configObject == null)
            throw new ArgumentNullException();

        _moveSpeed = _configObject.MoveSpeed;
        _startSpeed = _moveSpeed;
    }

    private void Update()
    {
        Move(Direction);
    }

    public void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
        {
            direction = Vector2.zero;
            return;
        }

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(direction.x, 0, direction.y) * scaledMoveSpeed;
        transform.Translate(offset, Space.World);
    }

    public void ApplyBuff(float value)
    {
        _moveSpeed = _startSpeed * (1 + value);
    }
}