using System.Threading.Tasks;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateRepeat : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Repeat];
        }

        protected override void OnEnter() {
            base.OnEnter();
            PerformTransition();
        }

        async void PerformTransition() {

            await Task.Delay(_transitionTimings[0]);
            
            EventBus<FadeRequest>.Raise(new FadeRequest
            {
                shouldFade = false
            });
            
            await Task.Delay(_transitionTimings[1]);

            
            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            {
                type = UITransitionMessageTypes.Repeat
            });
            
            await Task.Delay(_transitionTimings[2]);
            

            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            {
                type = UITransitionMessageTypes.None
            });
            

            RequestTransition<GP_ActionState>();

        }
    }
}