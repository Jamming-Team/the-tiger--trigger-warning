using UnityEngine;
using UnityEngine.InputSystem;
using static TigerInputActions;

namespace Tiger {
    
    public interface IInputReader
    {
        void EnablePlayerActions();
        void DisablePlayerActions();
    }
    
    [CreateAssetMenu(fileName = "InputReader", menuName = "Tiger/InputReader", order = 0)]
    public class InputReader : ScriptableObject, IInputReader, IGameplayActions {

        public Vector2 mousePosition = Vector2.zero;
        public Vector2 mouseDelta => _inputActions.Gameplay.Delta.ReadValue<Vector2>();
        public bool rotateIsBeingPressed => _inputActions.Gameplay.Rotate.inProgress;
        public bool interactIsBeingPressed => _inputActions.Gameplay.Interact.inProgress;
        
        TigerInputActions _inputActions;

        
        public void EnablePlayerActions()
        {
            if (_inputActions == null)
            {
                _inputActions = new TigerInputActions();
                _inputActions.Gameplay.SetCallbacks(this);
            }
            _inputActions.Enable();
        }

        public void DisablePlayerActions()
        {
            _inputActions.Disable();
        }
        

        public void OnPosition(InputAction.CallbackContext context) {
            mousePosition = context.ReadValue<Vector2>();
        }

        public void OnRotate(InputAction.CallbackContext context) {
            
            // throw new System.NotImplementedException();
        }

        public void OnInteract(InputAction.CallbackContext context) {
            // throw new System.NotImplementedException();
        }

        public void OnPause(InputAction.CallbackContext context) {
            // throw new System.NotImplementedException();
        }

        public void OnDelta(InputAction.CallbackContext context) {
            // mouseDelta = context.ReadValue<Vector2>();
        }
    }
}