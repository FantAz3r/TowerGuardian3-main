using System.Collections;
using System.Linq;
using TowerGuardian.Factories;
using TowerGuardian.Infrastructure;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 30f; 
    [SerializeField] private float _zoomSpeed = 5f; 
    [SerializeField] private float _minDistance = 5f; 
    [SerializeField] private float _maxDistance = 15f; 

    [SerializeField] private Transform _player; 
    private float _currentDistance;
    private bool _isZooming = false;

    private int _rotationDirection = 0;
    private float _currentAngle = 0f;

    private void Start()
    {
        _player = ServiceLocator.Get<IGameFactory>().SceneContainer.Portals.First().transform;
        Vector3 offset = transform.position - _player.position;
        _currentDistance = new Vector2(offset.x, offset.z).magnitude;
        _currentDistance = Mathf.Clamp(_currentDistance, _minDistance, _maxDistance);
        _currentAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        HandleRotationInput();

        if (_isZooming == false && _rotationDirection != 0)
        {
            _currentAngle += _rotationSpeed * _rotationDirection * Time.deltaTime;
            _currentAngle = _currentAngle % 360f;
        }

        float rad = _currentAngle * Mathf.Deg2Rad;

        float camX = _player.position.x + Mathf.Cos(rad) * _currentDistance;
        float camZ = _player.position.z + Mathf.Sin(rad) * _currentDistance;

        float camY = _player.position.y + 5f;

        transform.position = new Vector3(camX, camY, camZ);
        Vector3 lookTarget = new Vector3(_player.position.x, _player.position.y + 1.5f, _player.position.z);
        transform.LookAt(lookTarget);

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DoZoom());
        }
    }

    private void HandleRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_rotationDirection == 1)
            {
                _rotationDirection = 0;
            }
            else if (_rotationDirection == 0)
            {
                _rotationDirection = -1;
            }
            else if (_rotationDirection == -1)
            {
                _rotationDirection = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_rotationDirection == -1)
            {
                _rotationDirection = 0;
            }
            else if (_rotationDirection == 0)
            {
                _rotationDirection = 1;
            }
            else if (_rotationDirection == 1)
            {
                _rotationDirection = 0;
            }
        }
    }

    private IEnumerator DoZoom()
    {
        _isZooming = true;

        float targetDistance = Random.value > 0.5f ? _maxDistance : _minDistance;

        while (Mathf.Abs(_currentDistance - targetDistance) > 0.1f)
        {
            _currentDistance = Mathf.MoveTowards(_currentDistance, targetDistance, _zoomSpeed * Time.deltaTime);

            float rad = _currentAngle * Mathf.Deg2Rad;
            float camX = _player.position.x + Mathf.Cos(rad) * _currentDistance;
            float camZ = _player.position.z + Mathf.Sin(rad) * _currentDistance;
            float camY = _player.position.y + 5f;
            transform.position = new Vector3(camX, camY, camZ);

            Vector3 lookTarget = new Vector3(_player.position.x, _player.position.y + 1.5f, _player.position.z);
            transform.LookAt(lookTarget);

            yield return null;
        }

        _isZooming = false;
    }
}

