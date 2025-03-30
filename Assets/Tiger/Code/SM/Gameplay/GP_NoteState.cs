using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_NoteState : GP_SceneState {
        protected override void OnUIButtonPressed(UIButtonPressed e) {
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Resume: {
                    RequestTransition<GP_TransitionStateCorrect>();
                    break;
                }
                case UIButtonTypes.Exit: {
                    RequestTransition<GP_PostGameState>();
                    break;
                }
            }
        }
    }
}