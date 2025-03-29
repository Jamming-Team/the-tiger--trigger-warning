using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Tiger {
    public class CameraController : MonoBehaviour {
        [SerializeField] InputReader _inputReader;
        [SerializeField] CinemachineCamera _cinemachineCamera;
        [SerializeField] CinemachineOrbitalFollow _orbitalFollow;

        [SerializeField] Vector2 _mouseSensitivity = new Vector2(4f, 2f); 
        

        void Awake() {
            _inputReader.EnablePlayerActions();
        }

        void Update() {
            if (_inputReader.rotateIsBeingPressed) {
                var deltaMove = _inputReader.mouseDelta * _mouseSensitivity.Multiply(y: -1) * Time.deltaTime;
                _orbitalFollow.HorizontalAxis.Value += deltaMove.x;
                _orbitalFollow.VerticalAxis.Value = Mathf.Clamp(_orbitalFollow.VerticalAxis.Value + deltaMove.y, 10, 45);
                GameManager.Instance.SetCursorStatus(GameManager.CursorStatusTypes.Hidden);
            }
            else {
                GameManager.Instance.SetCursorStatus(GameManager.CursorStatusTypes.Normal);
            }
        }
        
        
    }
}