using System.Collections;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 1.1f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _playerRigidbody;
    private WaitForSeconds _wait = new WaitForSeconds(0.05f);
    private Coroutine _groundCheckCoroutine;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _groundCheckCoroutine = StartCoroutine(GroundCheckRoutine());
    }

    private void OnDisable()
    {
        if (_groundCheckCoroutine != null)
            StopCoroutine(_groundCheckCoroutine);
    }

    private IEnumerator GroundCheckRoutine()
    {
        while (enabled)
        {
            RaycastHit hit;
            bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, _rayDistance, _groundLayer);

            if (isGrounded == false)
            {
                _playerRigidbody.useGravity = true;
            }
            else
            {
                _playerRigidbody.useGravity = false;
            }

            yield return _wait;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Для визуализации рейкаста в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _rayDistance);
    }
}