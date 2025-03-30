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

            CalculateZoom();
        }

        void CalculateZoom() {
            _orbitalFollow.Radius = Mathf.Clamp(_orbitalFollow.Radius + -_inputReader.mouseScroll * data.zoomSensitivity * Time.deltaTime,
                data.zoomConstraints.x, data.zoomConstraints.y);
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

        void DetectClick() {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerInteractive)) {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactive"))
                    {
                        var clickableObject = hit.collider.gameObject.GetComponent<ClickableObject>();
                        if (clickableObject != null)
                        {
                            clickableObject.ClickTrigger();
                        }
                    }
                }
            }
        }

        void RandomRotateCamera()
        {
            float horizontalRotation = UnityEngine.Random.Range(0f, 360f);
            float verticalRotation = UnityEngine.Random.Range(data.verticalAxisConstraint.x, data.verticalAxisConstraint.y);

            _orbitalFollow.HorizontalAxis.Value = horizontalRotation;
            _orbitalFollow.VerticalAxis.Value = verticalRotation;
        }



        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}