using System;
using UnityEngine;

namespace Tiger {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] SceneLoaderController _sceneLoader;
        [SerializeField] DataSO _dataSO;
        
        DataManager _dataManager;

        void Awake() {
            Application.targetFrameRate = 60;
            _dataManager = new DataManager(_dataSO);
        }


        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) RequestSceneLoad("MainMenu");
        }

        public void RequestSceneLoad(string sceneName) {
            _sceneLoader.LoadSceneGroup(sceneName);
        }

        public void RequestData(IVisitable requester) {
            _dataManager.TrySupply(requester);
        }
    }
}