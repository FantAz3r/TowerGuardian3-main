using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Mover : MonoBehaviour
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
        Move(MoveDirection);
    }
    
    public void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
        {
            direction = Vector2.zero;
            return;
        }

        float scaledMoveSpeed = _config.MoveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(direction.x, 0, direction.y) * scaledMoveSpeed;
        transform.Translate(offset, Space.World);
    }
}