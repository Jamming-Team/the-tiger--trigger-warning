using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiger {
    public class CoreBootstrapper {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static async void Init() {
            Debug.Log("Bootstrapper init");
            await SceneManager.LoadSceneAsync(SceneNames.CORE, LoadSceneMode.Additive);
        }
    }
}