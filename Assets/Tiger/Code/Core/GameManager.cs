using System;
using UnityEngine;

namespace Tiger {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] SceneLoaderController _sceneLoader;

        void Start() {
            Application.targetFrameRate = 60;
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) RequestSceneLoad("MainMenu");
        }

        public void RequestSceneLoad(string sceneName) {
            _sceneLoader.LoadSceneGroup(sceneName);
        }
    }
}