using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_PauseState : GP_SceneState {
        protected override void OnUIButtonPressed(UIButtonPressed e) {
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Pause: {
                    RequestTransition<GP_ActionState>();
                    break;
                }
                case UIButtonTypes.Exit: {
                    GameManager.Instance.RequestSceneLoad(SceneNames.MAIN_MENU);
                    break;
                }
            }
        }
    }
}