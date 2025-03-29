using System.Threading.Tasks;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateIntro : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Intro];
        }

        protected override void OnEnter() {
            base.OnEnter();
            PerformTransition();
        }

        async void PerformTransition() {

            EventBus<FadeRequest>.Raise(new FadeRequest
            {
                shouldFade = false
            });
            
            await Task.Delay(_transitionTimings[0]);

            
            // Spawn here
            
            await Task.Delay(_transitionTimings[1]);
            
            
            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            {
                type = UITransitionMessageTypes.Remember
            });
            
            await Task.Delay(_transitionTimings[2]);

            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            {
                type = UITransitionMessageTypes.None
            });
            
            await Task.Delay(_transitionTimings[3]);

            EventBus<FadeRequest>.Raise(new FadeRequest
            {
                shouldFade = true
            });
            
            await Task.Delay(_transitionTimings[4]);

            RequestTransition<GP_TransitionStateRepeat>();

        }
    }
}