using System.Collections.Generic;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionState : GP_SceneState {


        protected List<int> _transitionTimings;

        protected void RaiseFadeRequest(bool shouldFadeFlag) {
            EventBus<FadeRequest>.Raise(new FadeRequest {
                shouldFade = shouldFadeFlag
            });
        }

        protected void RaiseTransitionRequest(UITransitionMessageTypes typeToSet) {
            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg {
                type = typeToSet
            });
        }

    }
}