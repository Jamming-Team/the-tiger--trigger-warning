using System.Threading.Tasks;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_ActionState : GP_SceneState {

        // int iter = 0;
        
        protected override void OnEnter() {
            base.OnEnter();
            // Test();
        }
        
        protected override void OnUIButtonPressed(UIButtonPressed e) {
            if (!_core.freeActIsInAction) return;
            Debug.Log("OnUIButtonPressed : " + e);
            switch (e.buttonType) {
                case UIButtonTypes.Pause: {
                    RequestTransition<GP_PauseState>();
                    break;
                }
                case UIButtonTypes.Next: {
                    RequestTransition<GP_TransitionStateRepeat>();
                    break;
                }
            }
        }
        //
        // async void Test() {
        //     await Task.Delay(2000);
        //
        //     if (iter == 0) {
        //         RequestTransition<GP_TransitionStateCorrect>();
        //         iter++;
        //     }
        //     else {
        //         RequestTransition<GP_TransitionStateWrong>();
        //     }
        //     
        // }
        


    }
}