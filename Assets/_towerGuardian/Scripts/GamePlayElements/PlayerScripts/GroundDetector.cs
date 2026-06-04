using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _gravityMultiplier = 3f;
    [SerializeField] private float _rayDistance = 1.1f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, _rayDistance, _groundLayer);

        if (!isGrounded)
        {
            _playerRigidbody.useGravity = true;

            Vector3 extraGravity = Physics.gravity * _gravityMultiplier;
            _playerRigidbody.AddForce(extraGravity, ForceMode.Acceleration);
        }
        else
        {
            _playerRigidbody.useGravity = false;
            Vector3 velocity = _playerRigidbody.velocity;
            velocity.x = 0f;
            velocity.z = 0f;
            _playerRigidbody.velocity = velocity;
        }
    }
}