using UnityEngine;

namespace Tiger
{
    public class RotationObject : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private Vector3 _lastCameraPosition;

        void Start()
        {
            UpdateRotation();
        }

        void LateUpdate()
        {
            if (!IsCameraMoved()) return;
            
            UpdateRotation();
        }

        void UpdateRotation()
        {
            LookAtCamera();
            SetLastCameraPosition();
        }

        private void LookAtCamera()
        {
            transform.LookAt(_camera.transform);
        }

        private void SetLastCameraPosition()
        {
            _lastCameraPosition = _camera.transform.position;
        }

        private bool IsCameraMoved()
        {
            return _lastCameraPosition != _camera.transform.position;
        }
    }
}