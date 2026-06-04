using TowerGuardian.StaticData;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private MoveConfig _config;
    private bool _canRotate = true;
    public Vector2 CurrentDirection { get; private set; }

    public void SetDirection(Vector2 direction) => CurrentDirection = direction;

    private void Update()
    {
        if(_canRotate)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(CurrentDirection.x, 0f, CurrentDirection.y);

        if (direction.sqrMagnitude < 0.0001f)
            return;

        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.y;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, _config.RotationSpeed);

        transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
    }

    public void CanRotate(bool canRotate) => _canRotate = canRotate;
}
