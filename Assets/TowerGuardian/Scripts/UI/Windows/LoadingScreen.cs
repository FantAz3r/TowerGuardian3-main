using UnityEngine;

namespace TowerGuardian.Scripts.UI.Windows
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _image;
        [SerializeField]
        private float _rotationSpeed = 90f;

        private void Update()
        {
            float angle = _rotationSpeed * Time.deltaTime;
            _image.transform.Rotate(0f, 0f, angle);
        }
    }
}
