using UnityEngine;

namespace Tiger.MainMenu {
    public class MM_SettingsState : MM_SceneState {
        protected override void OnUIButtonPressed(UIButtonPressed e) {
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Back: {
                    RequestTransition<MM_MainState>();
                    break;
                }
            }
        }
    }
}