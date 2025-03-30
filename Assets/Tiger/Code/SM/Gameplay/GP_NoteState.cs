using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_NoteState : GP_SceneState {
        protected override void OnUIButtonPressed(UIButtonPressed e) {
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Next: {
                    switch (_core.noteState) {
                        case GP_SceneController.NoteStates.ViewUntilResume: {
                            RequestTransition<GP_TransitionStateCorrect>();
                            break;
                        }
                        case GP_SceneController.NoteStates.ViewUntilUpdate: {
                            _core.noteState = GP_SceneController.NoteStates.ViewUntilResume;
                            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
                            {
                                type = UITransitionMessageTypes.Remember
                            });
                            _core.FillInitial();
                            break;
                        }
                        case GP_SceneController.NoteStates.ViewUntilExit: {
                            RequestTransition<GP_PostGameState>();
                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
}