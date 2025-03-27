using System;
using UnityEngine;

namespace Tiger {
    public class GameManager : Singleton<GameManager> {
        
        [SerializeField] private SceneLoaderController _sceneLoader;

        public void RequestSceneLoad(string sceneName) {
            _sceneLoader.LoadSceneGroup(sceneName);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                RequestSceneLoad("MainMenu");
            }
        }
    }
}