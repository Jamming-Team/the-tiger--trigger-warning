using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionState : MM_SceneState {


        protected void RaiseFadeRequest(bool shouldFadeFlag) {
            EventBus<FadeRequest>.Raise(new FadeRequest {
                shouldFade = shouldFadeFlag
            });
        }
        
        
    }
}