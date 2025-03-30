using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiger {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] SceneLoaderController _sceneLoader;
        [SerializeField] DataSO _dataSO;
        
        DataManager _dataManager;

        public bool isLoading = false;

        protected override void Awake() {
            base.Awake();
            Application.targetFrameRate = 60;
            _dataManager = new DataManager(_dataSO);
        }

        
        bool IsSceneLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName && scene.isLoaded)
                    return true;
            }
            return false;
        }
        
        public void RequestSceneLoad(string sceneName) {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            if (isLoading)
                return;
            isLoading = true;  
            Debug.Log("Loading scene " + sceneName);
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