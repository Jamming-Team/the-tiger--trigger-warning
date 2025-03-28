using UnityEngine;

namespace Tiger.MainMenu {
    public class MM_MainState : MM_SceneState {

        protected override void OnUIButtonPressed(UIButtonPressed e) {
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Settings: {
                    RequestTransition<MM_SettingsState>();
                    break;
                }
            }
        }
    }
}