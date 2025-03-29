using System.Threading.Tasks;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateWrong : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Intro];
        }

        protected override void OnEnter() {
            base.OnEnter();
            PerformTransition();
        }

        async void PerformTransition() {


            
            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            {
                type = UITransitionMessageTypes.Wrong
            });
            
            await Task.Delay(_transitionTimings[0]);

            

            EventBus<FadeRequest>.Raise(new FadeRequest
            {
                shouldFade = true
            });
            
            await Task.Delay(_transitionTimings[1]);

            
            RequestTransition<GP_PostGameState>();

        }
    }
}