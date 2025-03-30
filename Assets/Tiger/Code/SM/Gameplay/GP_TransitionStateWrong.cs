using System.Collections;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateWrong : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Intro];
        }

        protected override void OnEnter() {
            base.OnEnter();
            StartCoroutine(PerformTransition());
        }

        IEnumerator PerformTransition() {
            
            
            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            {
                type = UITransitionMessageTypes.Wrong
            });
            
            yield return new WaitForSeconds(2f);


            

            EventBus<FadeRequest>.Raise(new FadeRequest
            {
                shouldFade = true
            });
            
            yield return new WaitForSeconds(2f);


            
            RequestTransition<GP_PostGameState>();
        }
        
    }
}