using UnityEngine;

namespace TowerGuardian.Scripts.UI.EnviromentUI
{
    public class UIFaceCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void OnEnable()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
    }
}