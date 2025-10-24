using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private ScriptableObject _configObject;
    private IMoveConfig _config;

    public Vector2 CurrentDirection { get; private set; }

    public void SetDirection(Vector2 direction) => CurrentDirection = direction;


    private void Awake()
    {
        _config = _configObject as IMoveConfig;

        if (_config == null)
            throw new ArgumentNullException(nameof(_config));
    }

    private void Update()
    {
        Rotate();
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
}
