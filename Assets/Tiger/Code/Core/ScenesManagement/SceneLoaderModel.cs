using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiger {
    public class SceneLoaderModel {
        private const int OPERATIONS_AMOUNT = 1;
        private const string CORE_SCENE_NAME = "Core";
        private const int TASK_DELAY = 100;

        public event Action<string> OnSceneLoaded = delegate { };
        public event Action<string> OnSceneUnloaded = delegate { };

        // private SceneGroup _activeSceneGroup;

        public async void LoadScene(String sceneName, IProgress<float> progress, bool keepCurrentActive = false) {
            
            var scenes = new List<string>();
            var activeScene1 = SceneManager.GetActiveScene().name;

            int sceneCount = SceneManager.sceneCount;

            for (var i = sceneCount - 1; i >= 0; i--) {
                var sceneAt = SceneManager.GetSceneAt(i);
                if (!sceneAt.isLoaded) continue;

                var sceneName1 = sceneAt.name;
                if ((keepCurrentActive && sceneAt.name == activeScene1) || sceneName1 == CORE_SCENE_NAME) continue;
                scenes.Add(sceneName1);



                foreach (var scene in scenes) {
                    await  SceneManager.UnloadSceneAsync(scene);

                    OnSceneUnloaded.Invoke(scene);
                }



                // Optional: UnloadUnusedAssets - unloads all unused assets from memory
                await Resources.UnloadUnusedAssets();
            }

            
            
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            // A way to have a delay with that `Task` stuff
            // await Task.Delay(TimeSpan.FromSeconds(1));

            OnSceneLoaded.Invoke(sceneName);


            // OnSceneLoaded.Invoke(sceneData.Name);


            Scene activeScene = SceneManager.GetSceneByName(sceneName);

            if (activeScene.IsValid()) {
                SceneManager.SetActiveScene(activeScene);
            }
            
            GameManager.Instance.isLoading = false;
        }

        private async void UnloadScenes(bool keepCurrentActive = false) {

        }
    }
    
  
    
}