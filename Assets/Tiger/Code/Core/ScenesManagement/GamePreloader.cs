using UnityEngine;

namespace Tiger {
    public class GamePreloader : MonoBehaviour {
        private void Start()
        {
            GameManager.Instance.RequestSceneLoad(SceneNames.MAIN_MENU);
        }
    }
}