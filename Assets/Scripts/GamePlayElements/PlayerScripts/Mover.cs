using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Mover : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;
    public Vector2 MoveDirection { get; private set; }

    public void SetDirection(Vector2 direction) => MoveDirection = direction;

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