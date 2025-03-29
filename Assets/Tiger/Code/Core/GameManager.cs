using System;
using UnityEngine;

namespace Tiger {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] SceneLoaderController _sceneLoader;
        [SerializeField] DataSO _dataSO;
        
        DataManager _dataManager;

        protected override void Awake() {
            base.Awake();
            Application.targetFrameRate = 60;
            _dataManager = new DataManager(_dataSO);
        }


        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) RequestSceneLoad("MainMenu");
        }

        public void RequestSceneLoad(string sceneName) {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            _sceneLoader.LoadSceneGroup(sceneName);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public void RequestData(IVisitable requester) {
            _dataManager.TrySupply(requester);
        }

        Vector2 _cursorPos = Vector2.zero;
        public void SetCursorStatus(CursorStatusTypes type) {
            switch (type) {
                case CursorStatusTypes.Normal: {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    
                    break;
                }
                case CursorStatusTypes.Hidden: {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                }
            }
        }
        
        public enum CursorStatusTypes {
            Normal,
            Hidden
        }
    }
}