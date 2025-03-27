using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Tiger {
    public class SceneLoaderController : MonoBehaviour {
        [SerializeField] private SceneLoaderView _view;
        private readonly SceneLoaderModel _model = new SceneLoaderModel();
        private float _progress = 0;

        void Awake() {
            _model.OnSceneLoaded += sceneName => Debug.Log("Loaded: " + sceneName);
            _model.OnSceneUnloaded += sceneName => Debug.Log(" Unloaded: " + sceneName);
        }

        public async Task LoadSceneGroup(string sceneName) {
            LoadingProgress progress = new LoadingProgress();

            // `target` is what's coming from the event 
            progress.Progressed += target => _progress = target;

            var tcs = new TaskCompletionSource<bool>();
            // void OnReadyToContinue() {
            //     _view.ReadyToContinue -= OnReadyToContinue;
            //     tcs.SetResult(true);
            // }
            // _view.ReadyToContinue += OnReadyToContinue;
            
            
            // _view.EnableLoadingCanvas();
            await _model.LoadScene(sceneName, progress);
            // await tcs.Task; // TODO: States in view for "PRESS ANY BUTTON"
            // _view.EnableLoadingCanvas(false);
        }
    }

    public class LoadingProgress : IProgress<float> {
        public event Action<float> Progressed;

        private const float ratio = 1f;

        public void Report(float value) {
            Progressed?.Invoke(value / ratio);
        }
    }

    public static class SceneNames {
        public const string CORE = "Core", MAIN_MENU = "MainMenu", GAMEPLAY = "Gameplay";
    }
}