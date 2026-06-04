using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.back);
    }
}
