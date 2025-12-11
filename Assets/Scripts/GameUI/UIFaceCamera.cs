using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera not found!");
        }
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}
