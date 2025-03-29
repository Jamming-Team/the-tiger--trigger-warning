using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Tiger {
    public class CameraController : MonoBehaviour, IVisitable {
        [SerializeField] InputReader _inputReader;
        [SerializeField] CinemachineOrbitalFollow _orbitalFollow;
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _layerInteractive;
        
        public DataSO.CameraData data { get; set; }

        void Start() {
            _inputReader.EnablePlayerActions();
            GameManager.Instance.RequestData(this);
        }

        void Update()
        {
            OnCameraRotation();
            DetectClick();
        }

        void OnCameraRotation()
        {
            if (_inputReader.rotateIsBeingPressed) {
                var deltaMove = _inputReader.mouseDelta * data.rotationSensitivity.Multiply(y: -1) * Time.deltaTime;
                _orbitalFollow.HorizontalAxis.Value += deltaMove.x;
                _orbitalFollow.VerticalAxis.Value = Mathf.Clamp(_orbitalFollow.VerticalAxis.Value + deltaMove.y,
                    data.verticalAxisConstraint.x, data.verticalAxisConstraint.y);
                GameManager.Instance.SetCursorStatus(GameManager.CursorStatusTypes.Hidden);
            }
            else {
                GameManager.Instance.SetCursorStatus(GameManager.CursorStatusTypes.Normal);
            }
        }

        private void DetectClick() {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,_layerInteractive)) {
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}