using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_PostGameState : GP_SceneState {

        
        protected override void OnEnter() {
            base.OnEnter();
        }

        protected override void OnUIButtonPressed(UIButtonPressed e) {
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Restart: {
                    GameManager.Instance.RequestSceneLoad(SceneNames.GAMEPLAY);
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