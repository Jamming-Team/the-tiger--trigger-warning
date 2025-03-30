using UnityEngine;

namespace Tiger.MainMenu {
    public class MM_MainState : MM_SceneState {

        protected override void OnUIButtonPressed(UIButtonPressed e) {
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Play: {
                    GameManager.Instance.RequestSceneLoad(SceneNames.GAMEPLAY);
                    break;
                }
                case UIButtonTypes.Settings: {
                    RequestTransition<MM_SettingsState>();
                    break;
                }
                case UIButtonTypes.HowToPlay: {
                    RequestTransition<MM_HowToPlayState>();
                    break;
                }
            }
        }
    }
}