using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task LoadScene(String sceneName, IProgress<float> progress, bool keepCurrentActive = false) {
            await UnloadScenes();

            var operationGroup = new AsyncOperationGroup(OPERATIONS_AMOUNT);

            await Task.Delay(1000);

            
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            // A way to have a delay with that `Task` stuff
            // await Task.Delay(TimeSpan.FromSeconds(1));

            OnSceneLoaded.Invoke(sceneName);

            operationGroup.Operations.Add(operation);

            // OnSceneLoaded.Invoke(sceneData.Name);

            while (!operationGroup.IsDone) {
                progress?.Report(operationGroup.Progress);
                await Task.Delay(TASK_DELAY);
            }

            Scene activeScene = SceneManager.GetSceneByName(sceneName);

            if (activeScene.IsValid()) {
                SceneManager.SetActiveScene(activeScene);
            }
        }

        private async Task UnloadScenes(bool keepCurrentActive = false) {
            var scenes = new List<string>();
            var activeScene = SceneManager.GetActiveScene().name;

            int sceneCount = SceneManager.sceneCount;

            for (var i = sceneCount - 1; i >= 0; i--) {
                var sceneAt = SceneManager.GetSceneAt(i);
                if (!sceneAt.isLoaded) continue;

                var sceneName = sceneAt.name;
                if ((keepCurrentActive && sceneAt.name == activeScene) || sceneName == CORE_SCENE_NAME) continue;
                scenes.Add(sceneName);

                // Create an AsyncOperationGroup
                var operationGroup = new AsyncOperationGroup(scenes.Count);

                foreach (var scene in scenes) {
                    var operation = SceneManager.UnloadSceneAsync(scene);
                    if (operation == null) continue;

                    operationGroup.Operations.Add(operation);

                    OnSceneUnloaded.Invoke(scene);
                }

                // Wait until all AsyncOpertations in the group are done
                while (!operationGroup.IsDone) {
                    await Task.Delay(TASK_DELAY); // delay to avoid tight loop
                }

                // Optional: UnloadUnusedAssets - unloads all unused assets from memory
                await Resources.UnloadUnusedAssets();
            }
        }
    }
    
    public readonly struct AsyncOperationGroup {
        public readonly List<AsyncOperation> Operations;

        public float Progress => Operations.Count == 0 ? 0 : Operations.Average(o => o.progress);
        public bool IsDone => Operations.All(o => o.isDone);

        public AsyncOperationGroup(int initialCapacity) {
            Operations = new List<AsyncOperation>(initialCapacity);
        }
    }
}