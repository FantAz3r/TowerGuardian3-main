using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Mover : MonoBehaviour, IBuffble
{
    [SerializeField] private MoveConfig _configObject;
    [SerializeField] private LayerMask _obstacleLayerMask;

    private float _rayDistance = 1f;
    private float _moveSpeed;
    private float _startSpeed;

    public Vector2 Direction { get; private set; }

    public void SetDirection(Vector2 direction) => Direction = direction;

    private void Awake()
    {
        if (_configObject == null)
            throw new ArgumentNullException();

        _startSpeed = _configObject.MoveSpeed;
        _moveSpeed = _startSpeed;
    } 

    private void Update()
    {
        Move(Direction);
    }

    public void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
        {
            return; 
        }

        Vector3 moveDir = new Vector3(direction.x, 0f, direction.y).normalized;
        Vector3 offset = new Vector3 (0, 1, 0);
        RaycastHit hit;
        bool isHit = Physics.Raycast(transform.position + offset, moveDir, out hit, _rayDistance, _obstacleLayerMask);

        Debug.DrawRay(transform.position + offset, moveDir * _rayDistance, isHit ? Color.red : Color.green);

        if (isHit)
        {
            return;
        }

        float moveStep = _moveSpeed * Time.deltaTime;
        transform.Translate(moveDir * moveStep, Space.World);
    }


    public void ApplyBuff(float value)
    {
        _moveSpeed = _startSpeed * (1 + value);
    }

    public void RemoveBuff()
    {
        _moveSpeed = _startSpeed;
    }

    public void EnableBuff()
    {
    }
}
