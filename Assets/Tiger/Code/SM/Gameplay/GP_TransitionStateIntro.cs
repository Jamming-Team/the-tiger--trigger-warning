using System.Collections;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateIntro : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Intro];
        }

        protected override void OnEnter() {
            base.OnEnter();
            StartCoroutine(PerformTransition());
        }


        IEnumerator PerformTransition() {
            
            EventBus<FadeRequest>.Raise(new FadeRequest
            {
                shouldFade = false
            });
            
            yield return new WaitForSeconds(2f);


            
            // Zoom Here
            

            
            
            EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            {
                type = UITransitionMessageTypes.Remember
            });
            
            // await Task.Delay(_transitionTimings[2]);
            //
            // EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
            // {
            //     type = UITransitionMessageTypes.None
            // });
            
            yield return new WaitForSeconds(1f);


            // EventBus<FadeRequest>.Raise(new FadeRequest
            // {
            //     shouldFade = true
            // });
            //
            // await Task.Delay(_transitionTimings[4]);

            _core.FillInitial();
            
            RequestTransition<GP_NoteState>();
        }

    }
}